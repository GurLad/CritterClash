using Godot;
using System;

public class StatsModAdd : AStatsMod
{
    public StatsModAdd(int health, int attack, int speed) : base(health, attack, speed) { }

    public override Stats Apply(Stats origin)
    {
        Stats result = new Stats(origin);
        result.Health += Health;
        result.Attack += Attack;
        result.Speed += Speed;
        return result;
    }
}
