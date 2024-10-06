using Godot;
using System;

public partial class WinLoseScreen : Control
{
    [Export] private Button Next;

    [Export] private float AnimTime = 1;

    private Vector2 BaseScale;
    private Interpolator Interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        BaseScale = Scale;
        Next.Disabled = true;
        Visible = false;
    }

    public void ShowWinLose()
    {
        Visible = true;
        Interpolator.Interpolate(1,
            new Interpolator.InterpolateObject(
                a => Scale = BaseScale * a,
                0,
                1,
                Easing.EaseOutBack));
        Interpolator.OnFinish = () => Next.Disabled = false;
    }
}
