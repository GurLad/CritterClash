using Godot;
using System;

public class StatsModMul : AStatsMod
{
    protected StatsModMul(int health, int attack, int speed) : base(health, attack, speed) { }

    public override Stats Apply(Stats origin)
    {
        Stats result = new Stats(origin);
        result.Health *= Health;
        result.Attack *= Attack;
        result.Speed *= Speed;
        return result;
    }

    protected override string StatToString(int stat)
    {
        return stat != 1 ? ("*" + stat) : "";
    }
}
