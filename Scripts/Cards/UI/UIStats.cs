using Godot;
using System;

public partial class UIStats : Control
{
    [Export] private Label Health { get; set; }
    [Export] private TextureRect HealthRect { get; set; }
    [Export] private Label Attack { get; set; }
    [Export] private TextureRect AttackRect { get; set; }
    [Export] private Label Speed { get; set; }
    [Export] private TextureRect SpeedRect { get; set; }

    public void Render(DisplayStats stats)
    {
        HealthRect.Visible = !string.IsNullOrEmpty(stats.Health);
        Health.Text = stats.Health;
        AttackRect.Visible = !string.IsNullOrEmpty(stats.Attack);
        Attack.Text = stats.Attack;
        SpeedRect.Visible = !string.IsNullOrEmpty(stats.Speed);
        Speed.Text = stats.Speed;
    }
}
