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
        new BodyPartRecord(
            BodyPartType.Head,
            2, "Brain",
            new StatsModAdd(0, 0, 0),
            new List<ATrigger>()
            {
                new TriggerBodyPartAttached(new EffectAddStatsMod("this", new StatsModAdd(0, 1, 0)))
            },
            "When Upgraded: Gain 1 Attack.",
            "The beauty is in your eyes now."),
        new BodyPartRecord(
            BodyPartType.Head,
            1, "Alien",
            new StatsModAdd(0, 0, 0),
            new List<ATrigger>()
            {
                new TriggerDealDamage(new EffectAddStatsMod("this", new StatsModAdd(0, 1, 0)))
            },
            "When Attacking: Gain 1 Attack.",
            "The beauty is in your eyes now."),
        new BodyPartRecord(
            BodyPartType.Head,
            3, "Bulldog",
            new StatsModAdd(0, 1, 0),
            new List<ATrigger>()
            {
                new TriggerTakeDamage(new EffectAddStatsMod("this", new StatsModAdd(1, 1, 0)))
            },
            "When Attacked: Gain 1 Attack and Health.",
            "The beauty is in your eyes now."),
        new BodyPartRecord(
            BodyPartType.Head,
            2, "Fly",
            new StatsModAdd(0, 0, 1),
            new List<ATrigger>(),
            "",
            "Bzzzzzzz."),
        new BodyPartRecord(
            BodyPartType.Head,
            2, "Cat",
            new StatsModAdd(0, 0, 2),
            new List<ATrigger>()
            {
                new TriggerEndTurn(new EffectAddStatsMod("this", new StatsModAdd(0, 0, -1)))
            },
            "On End Turn: Lose 1 Speed.",
            "Bzzzzzzz."),
        new BodyPartRecord(
            BodyPartType.Head,
            1, "Gnome",
            new StatsModAdd(1, 0, 0),
            new List<ATrigger>(),
            "",
            "Gnomeception!"),
        new BodyPartRecord(
            BodyPartType.Head,
            2, "Mosquito",
            new StatsModAdd(0, 1, 0),
            new List<ATrigger>()
            {
                new TriggerDealDamage(new EffectAddStatsMod("this", new StatsModAdd(1, 0, 0)))
            },
            "When Attacking: Gain 1 Health.",
            "The beauty is in your eyes now."),
        new BodyPartRecord(
            BodyPartType.Head,
            1, "Woodpecker",
            new StatsModAdd(0, 1, 0),
            new List<ATrigger>(),
            "",
            "Pecking holes in your skulls since 19XX."),

        // Arms
        new BodyPartRecord(
            BodyPartType.Arm,
            2, "Tentacle",
            new StatsModAdd(1, 1, 0),
            new List<ATrigger>(),
            "",
            "Another tentacle never hurt anyone."),
        new BodyPartRecord(
            BodyPartType.Arm,
            2, "Monkey Paw",
            new StatsModAdd(0, 3, 0),
            new List<ATrigger>()
            {
                new TriggerEndTurn(new EffectAddStatsMod("this", new StatsModAdd(0, -1, 0)))
            },
            "On End Turn: Lose 1 Attack.",
            "There's a monkey on your paw!"),
        new BodyPartRecord(
            BodyPartType.Arm,
            1, "Bear Arm",
            new StatsModAdd(0, 4, 0),
            new List<ATrigger>()
            {
                new TriggerEndTurn(new EffectAddStatsMod("this", new StatsModAdd(0, 0, -1)))
            },
            "On End Turn: Lose 1 Speed.",
            "There's a monkey on your paw!"),
        new BodyPartRecord(
            BodyPartType.Arm,
            1, "Cursor",
            new StatsModAdd(0, 1, 0),
            new List<ATrigger>(),
            "",
            "Have you ever stabbed your toe on a cursor? No? Just me?"),
        new BodyPartRecord(
            BodyPartType.Arm,
            1, "Skelly Arm",
            new StatsModAdd(0, 2, 0),
            new List<ATrigger>()
            {
                new TriggerTakeDamage(new EffectAddStatsMod("this", new StatsModAdd(-1, 0, 0)))
            },
            "When Attacked: Take 1 extra damage.",
            "Have you ever stabbed your toe on a cursor? No? Just me?"),
        new BodyPartRecord(
            BodyPartType.Arm,
            3, "Spike Arm",
            new StatsModAdd(0, 5, 0),
            new List<ATrigger>(),
            "",
            "Ow the edge."),
        new BodyPartRecord(
            BodyPartType.Arm,
            0, "T-Rex",
            new StatsModAdd(0, 0, 0),
            new List<ATrigger>(),
            "",
            "About as useful as it looks."),

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
            3, "Cockatrice",
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
        new BodyPartRecord(
            BodyPartType.Leg,
            2, "Frog",
            new StatsModAdd(2, 0, 1),
            new List<ATrigger>(),
            "",
            "This is my self-insert."),
        new BodyPartRecord(
            BodyPartType.Leg,
            2, "House Cat",
            new StatsModAdd(0, 0, 2),
            new List<ATrigger>()
            {
                new TriggerEndTurn(new EffectAddStatsMod("this", new StatsModAdd(0, 0, -1)))
            },
            "On End Turn: Lose 1 Speed.",
            "This is my self-insert."),
        new BodyPartRecord(
            BodyPartType.Leg,
            0, "Peg Leg",
            new StatsModAdd(0, 0, 0),
            new List<ATrigger>(),
            "",
            "About as useful as it looks."),
        new BodyPartRecord(
            BodyPartType.Leg,
            1, "Skelly Leg",
            new StatsModAdd(0, 0, 2),
            new List<ATrigger>()
            {
                new TriggerTakeDamage(new EffectAddStatsMod("this", new StatsModAdd(-1, 0, 0)))
            },
            "When Attacked: Take 1 extra damage.",
            "About as useful as it looks."),
        new BodyPartRecord(
            BodyPartType.Leg,
            3, "Spiky Leg",
            new StatsModAdd(0, 2, 1),
            new List<ATrigger>(),
            "",
            "\"Practical\" is just a word."),
    };
}
