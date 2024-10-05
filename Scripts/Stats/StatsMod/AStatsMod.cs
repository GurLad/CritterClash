using Godot;
using System;

public abstract class AStatsMod
{
    public int Health { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Speed { get; set; } = 0;

    public abstract Stats Apply(Stats origin);
}
