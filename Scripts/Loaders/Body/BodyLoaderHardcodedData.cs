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
    };
}

