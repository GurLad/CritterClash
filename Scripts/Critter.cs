using Godot;
using System;
using System.Collections.Generic;

public partial class Critter : Sprite2D
{
    public Body Body { get; private set; }
    private Dictionary<BodyPartType, List<Node2D>> BodyPartLocations { get; } = new Dictionary<BodyPartType, List<Node2D>>();

    public void Init(BodyRecord bodyRecord)
    {
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
        Sprite2D newSprite = (Sprite2D)part.Sprite.Duplicate();
        holder.AddChild(newSprite);
    }

    private void AnimateDealDamage(TriggerParameter<Body> @this, TriggerParameter<Body> target, TriggerParameter<int> damage)
    {
        // TBA
    }

    private void AnimateTakeDamage(TriggerParameter<Body> @this, TriggerParameter<Body> attacker, TriggerParameter<int> damage)
    {
        // TBA
    }

    private void AnimateDeath(TriggerParameter<Body> @this)
    {
        // TBA
        QueueFree();
    }
}
