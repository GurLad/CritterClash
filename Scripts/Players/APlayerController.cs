using Godot;
using System;

public abstract partial class APlayerController : Node
{
    [Export] protected GameFlow GameFlow { get; private set; }
    [Export] protected GameGrid GameGrid { get; private set; }
    [Export] public bool Enemy { get; private set; }
    [Export] public int StartingHealth { get; private set; }

    protected Deck Deck { get; set; }
    private int Health { get; set; }

    [Signal]
    public delegate void OnTakeDamageEventHandler(APlayerController player, int newHealth);

    [Signal]
    public delegate void OnDeathEventHandler(APlayerController player);

    [Signal]
    public delegate void OnFinishTurnEventHandler(APlayerController player);

    public override void _Ready()
    {
        base._Ready();
        GameFlow.ConnectPlayerController(this);
    }

    public void ConnectDeck(Deck deck)
    {
        Deck = deck;
        Health = StartingHealth;
        ConnectDeckInternal(deck);
    }

    public void BeginTurn()
    {
        Deck.BeginTurn();
        BeginTurnInternal();
    }

    public bool TakeDamage()
    {
        Health--;
        EmitSignal(SignalName.OnTakeDamage, this, Health);
        if (Health <= 0)
        {
            EmitSignal(SignalName.OnDeath, this);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void EmitFinishTurn()
    {
        EmitSignal(SignalName.OnFinishTurn, this);
    }

    protected abstract void BeginTurnInternal();
    protected abstract void ConnectDeckInternal(Deck deck);
}
