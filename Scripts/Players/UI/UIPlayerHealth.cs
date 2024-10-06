using Godot;
using System;

public partial class UIPlayerHealth : Control
{
    [Export] private APlayerController Player;
    [Export] private Control HealthContainer;
    [Export] private Label HealthLabel;
    [ExportCategory("Animation")]
    [Export] private float TakeDamageAnimTime = 0.5f;
    [Export] private float TakeDamageAnimScaleMod = 1.3f;
    [Export] private Color TakeDamageAnimModulate;

    private Interpolator Interpolator = new Interpolator();
    private Vector2 BaseScale;
    private Color BaseModulate;

    public override void _Ready()
    {
        base._Ready();
        HealthLabel.Text = Player.StartingHealth.ToString();
        BaseScale = HealthContainer.Scale;
        BaseModulate = HealthContainer.Modulate;
        Player.OnTakeDamage += OnTakeDamage;
        AddChild(Interpolator);
    }

    private void OnTakeDamage(APlayerController player, int newHealth)
    {
        HealthLabel.Text = newHealth.ToString();
        Interpolator.Interpolate(TakeDamageAnimTime,
            new Interpolator.InterpolateObject(
                a => HealthContainer.Scale = BaseScale * a,
                TakeDamageAnimScaleMod,
                1,
                Easing.EaseOutElastic),
            new Interpolator.InterpolateObject(
                a => HealthContainer.Modulate = BaseModulate * a + TakeDamageAnimModulate * (1 - a),
                0,
                1,
                Easing.EaseInOutSin));
    }
}
