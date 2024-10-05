using Godot;
using System;
using System.Collections.Generic;

public partial class GameGrid : Node2D
{
    // Exports
    [Export] private int MaxPlaceDistFromBase { get; set; } = 1;
    [Export] private PackedScene SceneCritter { get; set; }
    // Properties
    public Vector2I Size { get; } = new Vector2I(7, 3); // Hardcoded for now
    public Critter this[Vector2I pos] => Critters.Find(a => a.Tile == pos);

    private List<Critter> Critters { get; } = new List<Critter>();

    [Signal]
    public delegate void OnCritterPlacedEventHandler(Critter critter);

    public override void _Ready()
    {
        base._Ready();
    }

    public Critter GetFirstAvailableCritter(bool enemy)
    {
        List<Critter> available = Critters.FindAll(a => a.Enemy == enemy && !a.Acted);
        available.Sort((a, b) =>
        {
            return a.Tile.Y != b.Tile.Y ? a.Tile.Y.CompareTo(b.Tile.Y) :
                (enemy ? -a.Tile.X.CompareTo(b.Tile.X) : a.Tile.X.CompareTo(b.Tile.X));
        });
        return available.Count > 0 ? available[0] : null;
    }

    public (Vector2I NewTile, Critter Collision) TryMoveCritter(Critter critter)
    {
        bool enemy = critter.Enemy;
        Vector2I target = critter.Tile + (enemy ? Vector2I.Left : Vector2I.Right) * critter.Body.Stats.Speed;
        int sign = enemy ? -1 : 1;
        Critter collision = Critters.Find(a => a.Tile.X * sign >= target.X * sign && a.Tile.X * sign <= critter.Tile.X * sign);
        if (collision != null)
        {
            target.X = collision.Tile.X - sign;
        }
        return (target, collision);
    }

    public bool CanPlaceNewCritter(bool enemy, Vector2I pos, BodyRecord bodyRecord)
    {
        if ((pos.X >= MaxPlaceDistFromBase && !enemy) || (pos.Y < Size.X - MaxPlaceDistFromBase && enemy))
        {
            return false;
        }
        return this[pos] == null;
    }

    public void PlaceNewCritter(bool enemy, Vector2I pos, BodyRecord bodyRecord)
    {
        if (bodyRecord == null)
        {
            return;
        }
        Critter newCritter = SceneCritter.Instantiate<Critter>();
        AddChild(newCritter);
        newCritter.Init(enemy, pos, bodyRecord);
        Critters.Add(newCritter);
        EmitSignal(SignalName.OnCritterPlaced, newCritter);
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
        if (partRecord == null)
        {
            return;
        }
        this[pos].AttachPart(partRecord);
    }
}
