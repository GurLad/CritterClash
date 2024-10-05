using Godot;
using System;

public class TriggerDealDamage : ATrigger
{
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
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "target", target },
            { "damage", damage }
        });
    }
}
