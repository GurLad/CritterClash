using Godot;
using System;

public class TriggerTakeDamage : ATrigger
{
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
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "attacker", attacker },
            { "damage", damage }
        });
    }
}
