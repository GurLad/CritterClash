using Godot;
using System;
using System.Collections.Generic;

public record Body(Dictionary<BodyPartType, int> PartSlots, Stats BaseStats, List<ATrigger> BaseTriggers)
{
    public List<BodyPart> BodyParts { get; } = new List<BodyPart>();
    public Dictionary<BodyPartType, int> CurrentParts { get; } = new Dictionary<BodyPartType, int>();

    // Events
    public event Action OnBeginTurn;
    public event Action OnEndTurn;
    public event Action<TriggerParameter<Body>, TriggerParameter<int>> OnTakeDamage;
    public event Action<TriggerParameter<Body>, TriggerParameter<int>> OnDealDamage;
    public event Action<TriggerParameter<BodyPart>> OnBodyPartAttached;
    public event Action OnDeath;

    public int DamageTaken = 0;
    public Stats Stats
    {
        get
        {
            Stats result = new Stats(Stats);
            BodyParts.ForEach(a => result = a.StatsMod.Apply(result));
            result.Health -= DamageTaken;
            return result;
        }
    }

    public void Init()
    {
        for (int i = 0; i < (int)BodyPartType.EndMarker; i++)
        {
            CurrentParts.Add((BodyPartType)i, 0);
        }
    }

    public bool CanAttachPart(BodyPart part)
    {
        return CurrentParts[part.Type] < PartSlots[part.Type];
    }

    public void AttachPart(BodyPart part)
    {
        BodyParts.Add(part);
        CurrentParts[part.Type]++;
        OnBodyPartAttached?.Invoke(new TriggerParameter<BodyPart>(part));
    }

    public int DealDamage(Body target)
    {
        TriggerParameter<int> damage = new TriggerParameter<int>(Stats.Attack);
        OnDealDamage?.Invoke(new TriggerParameter<Body>(target), damage);
        return target.TakeDamage(target, damage);
    }

    private int TakeDamage(Body attacker, TriggerParameter<int> damage)
    {
        OnTakeDamage?.Invoke(new TriggerParameter<Body>(attacker), damage);
        DamageTaken -= damage.Data;
        if (DamageTaken >= Stats.Health)
        {
            Die();
        }
        return damage.Data;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // TBA
    }
}
