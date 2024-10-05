using Godot;
using System;
using System.Collections.Generic;

public partial class GameFlow : Node
{
    // Exports
    [Export] private GameGrid GameGrid { get; set; }
    // Properties
    public bool EnemyTurn { get; private set; } = false;

    private bool AutoBattling = false;
    private List<Critter> AnimatingCritters { get; } = new List<Critter>();

    public override void _Ready()
    {
        base._Ready();
        GameGrid.OnCritterPlaced += OnCritterPlaced;
    }

    public void FinishTurn()
    {
        AutoBattling = true;
        ExecuteAutoBattleStep();
    }

    public void BeginTurn(bool enemy)
    {
        AutoBattling = false;
        EnemyTurn = enemy;
    }

    private void ExecuteAutoBattleStep()
    {
        if (!AutoBattling)
        {
            GD.PrintErr("[GameFlow]: Trying to do an auto-battle step outside the auto-battle phase!");
        }
        Critter nextCritter = GameGrid.GetFirstAvailableCritter(EnemyTurn);
        if (nextCritter != null)
        {
            if (nextCritter.Body.Stats.Speed <= 0)
            {
                nextCritter.Acted = true;
                ExecuteAutoBattleStep();
            }
            else
            {
                (Vector2I NewTile, Critter Collision) action = GameGrid.TryMoveCritter(nextCritter);
                nextCritter.Tile = action.NewTile;
                if (action.Collision != null && action.Collision.Enemy != nextCritter.Enemy)
                {
                    nextCritter.Body.DealDamage(action.Collision.Body);
                }
            }
        }
        else
        {
            BeginTurn(!EnemyTurn);
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

    private void OnCritterPlaced(Critter critter)
    {
        critter.OnBeginAnimation += OnCritterBeginAnimation;
        critter.OnFinishAnimation += OnCritterFinishAnimation;
        critter.Body.OnDeath += (b) => OnCritterDied(critter);
    }

    private void OnCritterDied(Critter critter)
    {
        if (AnimatingCritters.Contains(critter))
        {
            AnimatingCritters.Remove(critter);
            if (AnimatingCritters.Count <= 0)
            {
                ExecuteAutoBattleStep();
            }
        }
    }
}
