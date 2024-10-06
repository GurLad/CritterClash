using Godot;
using System;

public partial class UILevelInfo : Control
{
    [Export] private Control InputBlocker;
    [Export] private Label Title;
    [Export] private Label Text;
    [Export] private Button Start;
    [Export] private GameFlow GameFlow;

    [Export] private float AnimTime = 1;

    private Vector2 BaseScale;
    private Interpolator Interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        BaseScale = Scale;
        AddChild(Interpolator);
        Start.Disabled = true;
        if (GameFlow.Inited)
        {
            OnReadyToStart();
        }
        else
        {
            GameFlow.OnReadyToStart += OnReadyToStart;
        }
    }

    private void OnReadyToStart()
    {
        Title.Text = GameFlow.LevelData.Name;
        Text.Text = GameFlow.LevelData.Description;
        Interpolator.Interpolate(1,
            new Interpolator.InterpolateObject(
                a => Scale = BaseScale * a,
                0,
                1,
                Easing.EaseInBack));
        Interpolator.OnFinish = () => Start.Disabled = false;
    }

    public void FinishTutorial()
    {
        Start.Disabled = true;
        Interpolator.Interpolate(1,
            new Interpolator.InterpolateObject(
                a => Scale = BaseScale * a,
                1,
                0,
                Easing.EaseInBack));
        Interpolator.OnFinish = () =>
        {
            InputBlocker.QueueFree();
            GameFlow.BeginTurn(false);
        };
    }
}
