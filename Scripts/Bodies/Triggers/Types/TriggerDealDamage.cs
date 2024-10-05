using Godot;
using System;
using System.Collections.Generic;

public class TriggerDealDamage : ATrigger
{
    public TriggerDealDamage(List<AEffect> effects) : base(effects) { }

    public TriggerDealDamage(AEffect effect) : base(effect) { }

    protected override void ConnectInternal(Body body)
    {
        body.OnDealDamage += DealDamage;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnDealDamage -= DealDamage;
    }

    private void DealDamage(TriggerParameter<Body> @this, TriggerParameter<Body> target, TriggerParameter<int> damage)
    {
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "target", target },
            { "damage", damage }
        });
    }
}
