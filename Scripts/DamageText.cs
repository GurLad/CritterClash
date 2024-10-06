using Godot;
using System;

public partial class DamageText : Label
{
    [Export]
    private Label display;
    [Export]
    private float UpSpeed = 5;
    [Export]
    private float DisplayTime = 1;
    [Export]
    private float FadeTime = 0.2f;
    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
    }

    public void Display(int amount, Vector2 pos)
    {
        display.Position = pos;
        display.Text = amount.ToString();
        interpolator.Delay(DisplayTime);
        interpolator.OnFinish = () =>
        {
            interpolator.Interpolate(FadeTime,
                new Interpolator.InterpolateObject(
                    a => display.AddThemeColorOverride("font_color", new Color(a, a, a, a)),
                    1,
                    0),
                new Interpolator.InterpolateObject(
                    a => display.AddThemeColorOverride("font_outline_color", new Color(a, a, a, a)),
                    1,
                    0));
            interpolator.OnFinish = () => QueueFree();
        };
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        display.Position += Vector2.Up * UpSpeed * (float)delta;
    }
}
