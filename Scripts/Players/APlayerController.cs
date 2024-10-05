using Godot;
using System;

public abstract partial class APlayerController : Node
{
    [Export] protected GameFlow GameFlow { get; private set; }
    [Export] protected GameGrid GameGrid { get; private set; }
    [Export] public bool Enemy { get; private set; }

    protected Deck Deck { get; set; }

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
    }

    public void BeginTurn()
    {
        Deck.BeginTurn();
        BeginTurnInternal();
    }

    protected void EmitFinishTurn()
    {
        EmitSignal(SignalName.OnFinishTurn, this);
    }

    protected abstract void BeginTurnInternal();
    protected abstract void ConnectDeckInternal(Deck deck);
}
