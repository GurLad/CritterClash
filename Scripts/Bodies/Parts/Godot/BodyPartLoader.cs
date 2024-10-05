using Godot;
using System;

public partial class BodyPartLoader : Node
{
    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child is Sprite2D sprite)
            {
                BodyPartRecord record = BodyPartRecords.Find(a => a.Name == child.Name);
                if (record != null)
                {
                    record.AttachSprite(sprite);
                }
                else
                {
                    GD.PrintErr("[BodyPartLoader]: Sprite without a record!");
                }
            }
            else
            {
                GD.PrintErr("[BodyPartLoader]: Invalid sprite!");
            }
        }
        if (BodyPartRecords.FindIndex(a => !a.Inited) >= 0)
        {
            GD.PrintErr("[BodyPartLoader]: Record without a sprite!");
        }
    }
}
