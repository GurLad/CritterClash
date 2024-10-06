using Godot;
using System;

public partial class PlayerHuman : APlayerController
{
    [Export] private UICardPlacementGrid PlacementGrid;
    [Export] private UIHand Hand;
    [Export] private Control PlayerUI;
    [Export] private Control MouseBlocker;
    [ExportCategory("Animation")]
    [Export] private float AnimationTime = 0.3f;
    [Export] private Vector2 AnimationPosOffset = Vector2.Down * 121;

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
                BasePosition,
                BasePosition + AnimationPosOffset,
                Easing.EaseOutQuad));
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
                BasePosition + AnimationPosOffset,
                BasePosition,
                Easing.EaseInQuad));
        Interpolator.OnFinish = () => MouseBlocker.Visible = false;
    }

    protected override void ConnectDeckInternal(Deck deck)
    {
        PlacementGrid.Init(deck);
        Hand.Init(deck);
    }
}
