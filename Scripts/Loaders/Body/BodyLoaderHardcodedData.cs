using Godot;
using System;
using System.Collections.Generic;

public partial class BodyLoader : AGameLoader<BodyLoader, BodyRecord>
{
    public override List<BodyRecord> Records => new List<BodyRecord>(BodyRecords);

    protected List<BodyRecord> BodyRecords { get; } = new List<BodyRecord>()
    {
        new BodyRecord(
            2, "Ringabod",
            new Stats(2, 2, -1),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms, 2 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            2, "Snail",
            new Stats(4, 0, 1),
            new List<ATrigger>(),
            "1 * Head",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            5, "Angry Snail",
            new Stats(2, 2, 1),
            new List<ATrigger>(),
            "1 * Head",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            3, "Bee",
            new Stats(1, 1, 1),
            new List<ATrigger>(),
            "1 * Head",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            2, "Biped Blob",
            new Stats(1, 0, 0),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms, 2 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            1, "Butterfly",
            new Stats(1, 0, 1),
            new List<ATrigger>(),
            "1 * Head",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            3, "Flutter Fly",
            new Stats(1, 0, 3),
            new List<ATrigger>(),
            "1 * Head",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            3, "Scorpion",
            new Stats(1, 2, 0),
            new List<ATrigger>(),
            "2 * Arms, 4 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            2, "Dry Scorpion",
            new Stats(2, 0, 0),
            new List<ATrigger>(),
            "2 * Arms, 4 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            1, "4-Leg Mammal",
            new Stats(1, 0, 0),
            new List<ATrigger>(),
            "4 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            2, "Land Shark",
            new Stats(1, 1, 0),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms, 1 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            4, "Sky Shark",
            new Stats(1, 1, 1),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms, 1 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            3, "Merworm",
            new Stats(2, 0, 1),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            1, "Turtle",
            new Stats(4, 0, -1),
            new List<ATrigger>(),
            "1 * Head, 2 * Arms, 2 * Legs",
            "A very strong and tough body! Good luck getting it to move, though."),
        new BodyRecord(
            3, "Ninja Turtle",
            new Stats(3, 0, 1),
            new List<ATrigger>()
            {
                new TriggerEndTurn(new EffectAddStatsMod("this", new StatsModAdd(0, 0, -1))),
                new TriggerBodyPartAttached(new EffectAddStatsMod("this", new StatsModAdd(0, 0, 2))),
            },
            "1 * Head, 2 * Arms, 2 * Legs\nEnd Turn: -1 Speed.\nUpgraded: +2 Speed.",
            "A very strong and tough body! Good luck getting it to move, though."),
    };
}

