using Godot;
using System;

public partial class UIPlayerGnome : Sprite2D
{
    [Export] private APlayerController Player;
    [ExportCategory("Sprites")]
    [Export] private Texture2D Base;
    [Export] private Texture2D Hurt;
    [Export] private Texture2D Dead;
    [ExportCategory("Animation")]
    [Export] private float HurtTime = 0.5f;
    [Export] private Color HurtModulate;

    private bool IsDead { get; set; } = false;
    private Color BaseModulate { get; set; }
    private Interpolator Interpolator { get; set; } = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        BaseModulate = Modulate;
        Texture = Base;
        AddChild(Interpolator);
        Player.OnTakeDamage += OnTakeDamage;
        Player.OnDeath += OnDeath;
    }

    private void OnDeath(APlayerController player)
    {
        IsDead = true;
        Texture = Dead;
        Interpolator.Interpolate(HurtTime,
            new Interpolator.InterpolateObject(
                a => Modulate = BaseModulate * a + HurtModulate * (1 - a),
                0,
                1,
                Easing.EaseInOutSin));
    }

    private void OnTakeDamage(APlayerController player, int newHealth)
    {
        if (IsDead)
        {
            return;
        }
        Texture = Hurt;
        Interpolator.Interpolate(HurtTime,
            new Interpolator.InterpolateObject(
                a => Modulate = BaseModulate * a + HurtModulate * (1 - a),
                0,
                1,
                Easing.EaseInOutSin));
        Interpolator.OnFinish = () => Texture = IsDead ? Dead : Base;
    }
}
