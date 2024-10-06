using Godot;
using System;

public partial class UIPlayerDeath : Control
{
    [Export] private APlayerController Player;
    [Export] private float FadeInTime = 0.5f;

    private Interpolator Interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(Interpolator);
        Player.OnDeath += OnDeath;
        Visible = false;
    }

    private void OnDeath(APlayerController player)
    {
        Visible = true;
        Interpolator.Interpolate(FadeInTime,
            new Interpolator.InterpolateObject(
                a => Modulate = new Color(Modulate, a),
                Modulate.A,
                1));
    }
}
