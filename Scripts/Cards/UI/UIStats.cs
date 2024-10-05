using Godot;
using System;

public partial class UIStats : Node
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
        Health.Position += Vector2.Right * (stats.Health.Length > 1 ? 1 : 0);
        AttackRect.Visible = !string.IsNullOrEmpty(stats.Health);
        Attack.Text = stats.Health;
        Attack.Position += Vector2.Right * (stats.Attack.Length > 1 ? 1 : 0);
        SpeedRect.Visible = !string.IsNullOrEmpty(stats.Health);
        Speed.Text = stats.Health;
        Speed.Position += Vector2.Right * (stats.Speed.Length > 1 ? 1 : 0);
    }
}
