using Godot;
using System;

public partial class BodyLoader : Node
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
                    GD.PrintErr("[BodyPartLoader]: Loader node without a record!");
                }
            }
            else
            {
                GD.PrintErr("[BodyPartLoader]: Invalid loader node!");
            }
        }
        if (BodyRecords.FindIndex(a => !a.Inited) >= 0)
        {
            GD.PrintErr("[BodyPartLoader]: Record without a loader node!");
        }
    }
}
