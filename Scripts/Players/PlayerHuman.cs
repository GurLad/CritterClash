using Godot;
using System;

public partial class PlayerHuman : APlayerController
{
    [Export] private UICardPlacementGrid PlacementGrid;
    [Export] private UIHand Hand;
    [Export] private Control PlayerUI;
    [Export] private Control MouseBlocker;
    [ExportCategory("Animation")]
    [Export] private float AnimationTime = 1;
    [Export] private Vector2 AnimationPosOffset = Vector2.Down * 150;

    private Interpolator Interpolator = new Interpolator();
    private Vector2 BasePosition;

    public override void _Ready()
    {
        AddChild(Interpolator);
        BasePosition = PlayerUI.Position;
        PlayerUI.Position = BasePosition + AnimationPosOffset;
        PlayerUI.Visible = false;
        base._Ready();
    }

    public void FinishTurn()
    {
        MouseBlocker.Visible = true;
        Interpolator.Interpolate(AnimationTime,
            new Interpolator.InterpolateObject(
                a => PlayerUI.Position = a,
                PlayerUI.Position,
                BasePosition + AnimationPosOffset,
                Easing.EaseInSin));
        Interpolator.OnFinish = () =>
        {
            MouseBlocker.Visible = false;
            PlayerUI.Visible = false;
            EmitFinishTurn();
        };
    }

    protected override void BeginTurnInternal()
    {
        MouseBlocker.Visible = true;
        PlayerUI.Visible = true;
        Interpolator.Interpolate(AnimationTime,
            new Interpolator.InterpolateObject(
                a => PlayerUI.Position = a,
                PlayerUI.Position,
                BasePosition,
                Easing.EaseInSin));
        Interpolator.OnFinish = () => MouseBlocker.Visible = false;
    }

    protected override void ConnectDeckInternal(Deck deck)
    {
        PlacementGrid.Init(deck);
        Hand.Init(deck);
    }
}
