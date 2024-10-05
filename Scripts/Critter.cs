using Godot;
using System;
using System.Collections.Generic;

public partial class Critter : Sprite2D
{
    // Exports
    [Export] private Color BaseModulate;
    [Export] private Color ActedModulate;
    // Properties
    public Body Body { get; private set; }
    public bool Enemy { get; private set; }
    private Vector2I _tile;
    public Vector2I Tile
    {
        get => _tile;
        set
        {
            AnimateMove(value);
            _tile = value;
        }
    }
    private bool _busy = false;
    public bool Busy => _busy || ActionQueue.Count > 0;
    private bool _acted = false;
    public bool Acted
    {
        get => _acted;
        set
        {
            _acted = value;
            Modulate = value ? ActedModulate : BaseModulate;
        }
    }

    private Dictionary<BodyPartType, List<Node2D>> BodyPartLocations { get; } = new Dictionary<BodyPartType, List<Node2D>>();
    private Queue<Action> ActionQueue { get; } = new Queue<Action>();

    [Signal]
    public delegate void OnFinishAnimationEventHandler(Critter critter);

    [Signal]
    public delegate void OnBeginAnimationEventHandler(Critter critter);

    public void Init(bool enemy, Vector2I tile, BodyRecord bodyRecord)
    {
        _tile = tile;
        Enemy = enemy;
        Position = Tile.ToPhysicalLocation();
        Body = new Body(bodyRecord);
        for (int i = 0; i < (int)BodyPartType.EndMarker; i++)
        {
            BodyPartLocations.Add((BodyPartType)i, new List<Node2D>());
        }
        Texture = Body.Record.Texture;
        Body.Record.PartSlots.ForEach(a =>
        {
            Node2D holder = new Node2D();
            AddChild(holder);
            holder.Position = a.Position;
            holder.ZIndex = a.Foreground ? 1 : -1;
            BodyPartLocations[a.Type].Add(holder);
        });
        Body.OnDealDamage += AnimateDealDamage;
        Body.OnTakeDamage += AnimateTakeDamage;
        Body.OnDeath += AnimateDeath;
    }

    public bool CanAttachPart(BodyPartRecord part)
    {
        return Body.CanAttachPart(part);
    }

    public void AttachPart(BodyPartRecord part)
    {
        Body.AttachPart(part);
        Node2D holder = BodyPartLocations[part.Type].RandomItemInList();
        BodyPartLocations[part.Type].Remove(holder);
        Sprite2D newSprite = (Sprite2D)part.Sprite.Duplicate();
        holder.AddChild(newSprite);
        newSprite.Visible = true;
    }

    private void AnimateMove(Vector2I target)
    {
        if (!PreAnimate(() => AnimateMove(target))) return;
        // TBA
        Position = Tile.ToPhysicalLocation();
        PostAnimate();
    }

    private void AnimateDealDamage(TriggerParameter<Body> @this, TriggerParameter<Body> target, TriggerParameter<int> damage)
    {
        if (!PreAnimate(() => AnimateDealDamage(@this, target, damage))) return;
        // TBA
        PostAnimate();
    }

    private void AnimateTakeDamage(TriggerParameter<Body> @this, TriggerParameter<Body> attacker, TriggerParameter<int> damage)
    {
        if (!PreAnimate(() => AnimateTakeDamage(@this, attacker, damage))) return;
        // TBA
        PostAnimate();
    }

    private void AnimateDeath(TriggerParameter<Body> @this)
    {
        if (!PreAnimate(() => AnimateDeath(@this))) return;
        // TBA
        QueueFree();
        PostAnimate();
    }

    private bool PreAnimate(Action callback)
    {
        if (Busy)
        {
            ActionQueue.Enqueue(callback);
            return false;
        }
        _busy = true;
        EmitSignal(SignalName.OnBeginAnimation, this);
        return true;
    }

    private void PostAnimate()
    {
        if (ActionQueue.Count > 0)
        {
            ActionQueue.Dequeue().Invoke();
        }
        else
        {
            _busy = false;
            EmitSignal(SignalName.OnFinishAnimation, this);
        }
    }
}
