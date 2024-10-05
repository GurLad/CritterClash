using Godot;
using System;
using System.Collections.Generic;

public class Body
{
    public BodyRecord Record { get; init; }
    public List<BodyPartRecord> BodyParts { get; } = new List<BodyPartRecord>();
    public Dictionary<BodyPartType, int> CurrentParts { get; } = new Dictionary<BodyPartType, int>();

    // Events
    public event Action<TriggerParameter<Body>> OnBeginTurn;
    public event Action<TriggerParameter<Body>> OnEndTurn;
    public event Action<TriggerParameter<Body>, TriggerParameter<Body>, TriggerParameter<int>> OnTakeDamage;
    public event Action<TriggerParameter<Body>, TriggerParameter<Body>, TriggerParameter<int>> OnDealDamage;
    public event Action<TriggerParameter<Body>, TriggerParameter<BodyPartRecord>> OnBodyPartAttached;
    public event Action<TriggerParameter<Body>> OnDeath;

    public int DamageTaken { get; private set; } = 0;
    public Stats Stats
    {
        get
        {
            Stats result = new Stats(Record.BaseStats);
            BodyParts.ForEach(a => result = a.StatsMod.Apply(result));
            result.Health -= DamageTaken;
            return result;
        }
    }

    public Body(BodyRecord record)
    {
        Record = record;
        for (int i = 0; i < (int)BodyPartType.EndMarker; i++)
        {
            CurrentParts.Add((BodyPartType)i, 0);
        }
        Record.BaseTriggers.ForEach(a => a.Connect(this));
    }

    public bool CanAttachPart(BodyPartRecord part)
    {
        return CurrentParts[part.Type] < Record.PartSlots[part.Type];
    }

    public void AttachPart(BodyPartRecord part)
    {
        BodyParts.Add(part);
        part.Triggers.ForEach(a => a.Connect(this));
        CurrentParts[part.Type]++;
        OnBodyPartAttached?.Invoke(new TriggerParameter<Body>(this), new TriggerParameter<BodyPartRecord>(part));
    }

    public void BeginTurn()
    {
        OnBeginTurn?.Invoke(new TriggerParameter<Body>(this));
    }

    public void EndTurn()
    {
        OnEndTurn?.Invoke(new TriggerParameter<Body>(this));
    }

    public int DealDamage(Body target)
    {
        TriggerParameter<int> damage = new TriggerParameter<int>(Stats.Attack);
        OnDealDamage?.Invoke(new TriggerParameter<Body>(this), new TriggerParameter<Body>(target), damage);
        return target.TakeDamage(target, damage);
    }

    private int TakeDamage(Body attacker, TriggerParameter<int> damage)
    {
        OnTakeDamage?.Invoke(new TriggerParameter<Body>(this), new TriggerParameter<Body>(attacker), damage);
        DamageTaken -= damage.Data;
        if (DamageTaken >= Stats.Health)
        {
            Die();
        }
        return damage.Data;
    }

    private void Die()
    {
        OnDeath?.Invoke(new TriggerParameter<Body>(this));
        Record.BaseTriggers.ForEach(a => a.Disconnect(this));
        BodyParts.ForEach(a => a.Triggers.ForEach(b => b.Disconnect(this)));
        // TBA
    }
}
