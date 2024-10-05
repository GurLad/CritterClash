using Godot;
using System;

public abstract partial class APlayerController : Node
{
    [Export] private GameFlow GameFlow;
    [Export] public bool Enemy { get; private set; }

    [Signal]
    public delegate void OnFinishTurnEventHandler(APlayerController player);

    public override void _Ready()
    {
        base._Ready();
        GameFlow.ConnectPlayerController(this);
    }

    public abstract void BeginTurn();

    protected void EmitFinishTurn()
    {
        EmitSignal(SignalName.OnFinishTurn, this);
    }
}
