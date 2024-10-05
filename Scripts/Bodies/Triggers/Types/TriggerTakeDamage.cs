using Godot;
using System;
using System.Collections.Generic;

public class TriggerTakeDamage : ATrigger
{
    public TriggerTakeDamage(List<AEffect> effects) : base(effects) { }

    public TriggerTakeDamage(AEffect effect) : base(effect) { }

    protected override void ConnectInternal(Body body)
    {
        body.OnTakeDamage += TakeDamage;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnTakeDamage -= TakeDamage;
    }

    private void TakeDamage(TriggerParameter<Body> @this, TriggerParameter<Body> attacker, TriggerParameter<int> damage)
    {
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "attacker", attacker },
            { "damage", damage }
        });
    }
}
