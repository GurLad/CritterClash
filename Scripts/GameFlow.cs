using Godot;
using System;
using System.Collections.Generic;

public partial class GameFlow : Node
{
    // Exports
    [Export] private GameGrid GameGrid { get; set; }
    [Export] private WinLoseScreen WinScreen { get; set; }
    [Export] private WinLoseScreen LoseScreen { get; set; }
    // Properties
    public bool EnemyTurn { get; private set; } = false;

    private bool GameOver { get; set; } = false;
    public bool Inited { get; private set; } = false; // Whatevs
    private bool FinishedReady { get; set; } = false;
    private bool AutoBattling { get; set; } = false;
    private List<Critter> AnimatingCritters { get; } = new List<Critter>();
    private Dictionary<bool, APlayerController> Players { get; } = new Dictionary<bool, APlayerController>();
    public LevelData LevelData { get; private set; } = null; // Whatevs

    [Signal]
    public delegate void OnReadyToStartEventHandler();

    public override void _Ready()
    {
        base._Ready();
        GameGrid.OnCritterPlaced += OnCritterPlaced;
        GameGrid.OnCritterReachedBase += OnCritterReachedBase;
        FinishedReady = true;
        TryInit();
        //// TEMP DEBUG
        //GameGrid.PlaceNewCritter(false, Vector2I.One, BodyLoader.Get("Ringabod"));
        //GameGrid.AttachBodyPart(false, Vector2I.One, BodyPartLoader.Get("Cockatrice Leg"));
        //GameGrid.AttachBodyPart(false, Vector2I.One, BodyPartLoader.Get("Wheel"));
        //GameGrid.AttachBodyPart(false, Vector2I.One, BodyPartLoader.Get("Monkey Paw"));
        //GameGrid.AttachBodyPart(false, Vector2I.One, BodyPartLoader.Get("Tentacle Whip"));
        //GameGrid.AttachBodyPart(false, Vector2I.One, BodyPartLoader.Get("Beholder"));
        //GameGrid.PlaceNewCritter(true, Vector2I.One + Vector2I.Right * 4, BodyLoader.Get("Ringabod"));
        //GameGrid.AttachBodyPart(true, Vector2I.One + Vector2I.Right * 4, BodyPartLoader.Get("Cockatrice Leg"));
        //GameGrid.AttachBodyPart(true, Vector2I.One + Vector2I.Right * 4, BodyPartLoader.Get("Wheel"));
        //GameGrid.AttachBodyPart(true, Vector2I.One + Vector2I.Right * 4, BodyPartLoader.Get("Monkey Paw"));
        //GameGrid.AttachBodyPart(true, Vector2I.One + Vector2I.Right * 4, BodyPartLoader.Get("Tentacle Whip"));
        //GameGrid.AttachBodyPart(true, Vector2I.One + Vector2I.Right * 4, BodyPartLoader.Get("Beholder"));
        //BeginTurn(false);
    }

    public void ConnectPlayerController(APlayerController player)
    {
        player.OnFinishTurn += OnPlayerFinishTurn;
        Players.Add(player.Enemy, player);
        if (LevelData != null)
        {
            player.ConnectDeck(player.Enemy ? LevelData.cpuDeck : LevelData.humanDeck);
        }
        TryInit();
    }

    public void ConnectLevel(LevelData level)
    {
        LevelData = level;
        if (Players.ContainsKey(false))
        {
            Players[false].ConnectDeck(LevelData.humanDeck);
        }
        if (Players.ContainsKey(true))
        {
            Players[true].ConnectDeck(LevelData.cpuDeck);
        }
        TryInit();
    }

    public void FinishTurn()
    {
        if (GameOver)
        {
            return;
        }
        if (AutoBattling)
        {
            GD.PrintErr("[GameFlow]: Finished another turn during auto-battle!");
            return;
        }
        AutoBattling = true;
        ExecuteAutoBattleStep();
    }

    public void BeginTurn(bool enemy)
    {
        if (GameOver)
        {
            return;
        }
        AutoBattling = false;
        EnemyTurn = enemy;
        GameGrid.RefreshAllCritters();
        GameGrid.CrittersForEach(a => a.BeginTurn());
        Players[enemy].BeginTurn();
    }

    private void TryInit()
    {
        if (FinishedReady && Players.ContainsKey(false) && Players.ContainsKey(true) && LevelData != null)
        {
            EmitSignal(SignalName.OnReadyToStart);
        }
    }

    private void ExecuteAutoBattleStep()
    {
        if (GameOver)
        {
            return;
        }
        if (!AutoBattling)
        {
            GD.PrintErr("[GameFlow]: Trying to do an auto-battle step outside the auto-battle phase!");
        }
        Critter nextCritter = GameGrid.GetFirstAvailableCritter(EnemyTurn);
        if (nextCritter != null)
        {
            nextCritter.Acted = true;
            if (nextCritter.Body.BaseStats.Speed <= 0)
            {
                nextCritter.UpdateModulate();
                nextCritter.EndTurn();
                ExecuteAutoBattleStep();
            }
            else
            {
                nextCritter.OnFinishAnimation += OnActiveCritterFinishAnimation;
                (Vector2I NewTile, Critter Collision) action = GameGrid.TryMoveCritter(nextCritter);
                bool didAnything = false;
                if (nextCritter.Tile != action.NewTile)
                {
                    nextCritter.Tile = action.NewTile;
                    didAnything = true;
                }
                if (action.Collision != null && action.Collision.Enemy != nextCritter.Enemy && nextCritter.Body.BaseStats.Attack > 0)
                {
                    nextCritter.Attack(action.Collision);
                    didAnything = true;
                }
                if (!didAnything)
                {
                    OnActiveCritterFinishAnimation(nextCritter);
                    ExecuteAutoBattleStep();
                }
            }
        }
        else
        {
            BeginTurn(!EnemyTurn);
        }
    }

    private void OnPlayerFinishTurn(APlayerController player)
    {
        if (player.Enemy == EnemyTurn)
        {
            FinishTurn();
        }
        else
        {
            GD.PrintErr("Wrong player is trying to finish their turn!");
        }
    }

    private void OnCritterBeginAnimation(Critter critter)
    {
        if (!AnimatingCritters.Contains(critter))
        {
            AnimatingCritters.Add(critter);
        }
    }

    private void OnCritterFinishAnimation(Critter critter)
    {
        if (AnimatingCritters.Contains(critter))
        {
            AnimatingCritters.Remove(critter);
            if (AnimatingCritters.Count <= 0)
            {
                ExecuteAutoBattleStep();
            }
        }
        else
        {
            GD.PrintErr("A non-animating critter removed itself!");
        }
    }

    private void OnActiveCritterFinishAnimation(Critter critter)
    {
        critter.UpdateModulate();
        critter.EndTurn();
        critter.OnFinishAnimation -= OnActiveCritterFinishAnimation;
    }

    private void OnCritterPlaced(Critter critter)
    {
        critter.OnBeginAnimation += OnCritterBeginAnimation;
        critter.OnFinishAnimation += OnCritterFinishAnimation;
        critter.Body.OnDeath += (b) => OnCritterDied(critter);
    }

    private void OnCritterReachedBase(Critter critter)
    {
        GameGrid.RemoveCritter(critter);
        critter.ReachBase();
        if (Players[!critter.Enemy].TakeDamage())
        {
            // Stop everything...
            GameOver = true;
            if (!critter.Enemy)
            {
                WinScreen.ShowWinLose();
            }
            else
            {
                LoseScreen.ShowWinLose();
            }
        }
    }

    private void OnCritterDied(Critter critter)
    {
        //if (AnimatingCritters.Contains(critter))
        //{
        //    AnimatingCritters.Remove(critter);
        //    if (AnimatingCritters.Count <= 0)
        //    {
        //        ExecuteAutoBattleStep();
        //    }
        //}
        GameGrid.RemoveCritter(critter);
    }
}
