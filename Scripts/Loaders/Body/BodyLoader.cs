using Godot;
using System;

public partial class BodyLoader : AGameLoader<BodyLoader, BodyRecord>
{
    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child is BodyLoaderNode node)
            {
                BodyRecord record = BodyRecords.Find(a => a.Name == child.Name);
                if (record != null)
                {
                    record.AttachLoaderNode(node);
                }
                else
                {
                    GD.PrintErr("[BodyLoader]: Loader node without a record! " + node.Name);
                }
            }
            else
            {
                GD.PrintErr("[BodyLoader]: Invalid loader node!");
            }
        }
        if (BodyRecords.FindIndex(a => !a.Inited) >= 0)
        {
            GD.PrintErr("[BodyLoader]: Record without a loader node! " + BodyRecords.Find(a => !a.Inited));
        }
    }
}
