using Godot;
using System;
using System.Collections.Generic;

public class TriggerDeath : ATrigger
{
    public TriggerDeath(List<AEffect> effects) : base(effects) { }

    public TriggerDeath(AEffect effect) : base(effect) { }

    protected override void ConnectInternal(Body body)
    {
        body.OnDeath += Death;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnDeath -= Death;
    }

    private void Death(TriggerParameter<Body> @this)
    {
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
