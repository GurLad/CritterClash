using Godot;
using System;

public class StatsModAdd : AStatsMod
{
    public override Stats Apply(Stats origin)
    {
        Stats result = new Stats(origin);
        result.Health += Health;
        result.Attack += Attack;
        result.Speed += Speed;
        return result;
    }
}
