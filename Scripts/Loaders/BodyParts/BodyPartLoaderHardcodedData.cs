using Godot;
using System;
using System.Collections.Generic;

public partial class BodyPartLoader : AGameLoader<BodyPartLoader, BodyPartRecord>
{
    public override List<BodyPartRecord> Records => new List<BodyPartRecord>(BodyPartRecords);

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
            "When Attacking: Reduce the target's Attack by 1.",
            "The beauty is in your eyes now."),

        // Arms
        new BodyPartRecord(
            BodyPartType.Arm,
            2, "Tentacle Whip",
            new StatsModAdd(1, 1, 0),
            new List<ATrigger>(),
            "",
            "Another tentacle never hurt anyone."),
        new BodyPartRecord(
            BodyPartType.Arm,
            2, "Monkey Paw",
            new StatsModAdd(0, 4, 0),
            new List<ATrigger>()
            {
                new TriggerDealDamage(new EffectAddStatsMod("this", new StatsModAdd(0, -1, 0)))
            },
            "When Attacking: Lose 1 Attack.",
            "There's a monkey on your paw!"),

        // Legs
        new BodyPartRecord(
            BodyPartType.Leg,
            1, "Chicken Leg",
            new StatsModAdd(0, 0, 1),
            new List<ATrigger>(),
            "",
            "It won't turn you into a cockatrice."),
        new BodyPartRecord(
            BodyPartType.Leg,
            3, "Cockatrice Leg",
            new StatsModAdd(0, 0, 1),
            new List<ATrigger>()
            {
                new TriggerDealDamage(new EffectAddStatsMod("target", new StatsModAdd(0, 0, -1)))
            },
            "When Attacking: Reduce the target's Speed by 1.",
            "The leg of a stone-cold killer."),
        new BodyPartRecord(
            BodyPartType.Leg,
            2, "Wheel",
            new StatsModAdd(0, 0, 2),
            new List<ATrigger>(),
            "",
            "No invention needed!"),
    };
}
