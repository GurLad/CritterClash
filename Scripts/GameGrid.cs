using Godot;
using System;
using System.Collections.Generic;

public partial class GameGrid : Node2D
{
    private const int MAX_PLACE_DIST_FROM_BASE = 1;

    public Vector2I Size { get; } = new Vector2I(7, 3); // Hardcoded for now
    public Critter this[Vector2I pos] => Critters.Find(a => a.Position == pos);

    private List<Critter> Critters { get; } = new List<Critter>();

    public bool CanPlaceNewCritter(bool enemy, Vector2I pos, BodyRecord bodyRecord)
    {
        if ((pos.X >= MAX_PLACE_DIST_FROM_BASE && !enemy) || (pos.Y < Size.X - MAX_PLACE_DIST_FROM_BASE && enemy))
        {
            return false;
        }
        return this[pos] == null;
    }

    public void PlaceNewCritter(bool enemy, Vector2I pos, BodyRecord bodyRecord)
    {
        Critter newCritter = new Critter();
        AddChild(newCritter);
        newCritter.Init(enemy, pos, bodyRecord);
    }

    public bool CanAttachBodyPart(bool enemy, Vector2I pos, BodyPartRecord partRecord)
    {
        Critter target = this[pos];
        if (target == null)
        {
            return false;
        }
        return target.Enemy == enemy && target.CanAttachPart(partRecord);

    }

    public void AttachBodyPart(bool enemy, Vector2I pos, BodyPartRecord partRecord)
    {
        this[pos].AttachPart(partRecord);
    }
}
