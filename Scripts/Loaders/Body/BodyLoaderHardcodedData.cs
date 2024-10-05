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
            new Stats(3, 0, -1),
            new List<ATrigger>(),
            "",
            "A very durable body! Good luck getting it to move."),
    };
}

