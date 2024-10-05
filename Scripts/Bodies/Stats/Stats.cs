using Godot;
using System;

public class Stats
{
    public int Health { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Speed { get; set; } = 0;

    public Stats(int health, int attack, int speed)
    {
        Health = health;
        Attack = attack;
        Speed = speed;
    }

    public Stats(Stats origin)
    {
        Health = origin.Health;
        Attack = origin.Attack;
        Speed = origin.Speed;
    }

    public DisplayStats ToDisplayStats()
    {
        return new DisplayStats(Health.ToString(), Attack.ToString(), Speed.ToString());
    }
}
