using Godot;
using System;

public abstract partial class APlayerController : Node
{
    [Export] private GameFlow GameFlow;
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
}
