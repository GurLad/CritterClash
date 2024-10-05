using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public record BodyRecord(int Cost, string Name, Stats BaseStats, List<ATrigger> BaseTriggers, string Description, string FlavourText) : ILoadedType
{
    public List<(BodyPartType Type, Vector2I Position, bool Foreground)> PartSlots { get; } = new List<(BodyPartType Type, Vector2I Position, bool Foreground)>();
    public Texture2D Texture { get; private set; }
    public bool Inited { get; private set; }

    public void AttachLoaderNode(BodyLoaderNode loaderNode)
    {
        if (Inited)
        {
            GD.PrintErr("[BodyPartRecord]: Double init!");
        }
        // EndsWith("F")... How horrible... Even for me...
        loaderNode.Heads.ToList().ForEach(a => PartSlots.Add((BodyPartType.Head, a.Position.ToV2I(), a.Name.ToString().EndsWith("F"))));
        loaderNode.Arms.ToList().ForEach(a => PartSlots.Add((BodyPartType.Arm, a.Position.ToV2I(), a.Name.ToString().EndsWith("F"))));
        loaderNode.Legs.ToList().ForEach(a => PartSlots.Add((BodyPartType.Leg, a.Position.ToV2I(), a.Name.ToString().EndsWith("F"))));
        Texture = loaderNode.Texture;
        Inited = true;
    }
}
