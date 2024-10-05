using Godot;
using System;

public abstract class AStatsMod
{
    public int Health { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Speed { get; set; } = 0;

    protected AStatsMod(int health, int attack, int speed)
    {
        Health = health;
        Attack = attack;
        Speed = speed;
    }

    public abstract Stats Apply(Stats origin);
}
