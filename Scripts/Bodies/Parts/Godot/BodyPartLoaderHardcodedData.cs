using Godot;
using System;
using System.Collections.Generic;

public partial class BodyPartLoader : Node
{
    protected List<BodyPartRecord> BodyPartRecords { get; } = new List<BodyPartRecord>()
    {
        // Heads
        new BodyPartRecord(
            BodyPartType.Head,
            2, "Beholder",
            new StatsModAdd(0, 0, 0),
            new List<ATrigger>()
            {
                new TriggerDealDamage(new EffectAddStatsMod("target", new StatsModAdd(0, -1, 0)))
            },
            "When Attacking: Reduce the target's attack by 1.",
            "The beauty is in your eyes now.")
    };
}
