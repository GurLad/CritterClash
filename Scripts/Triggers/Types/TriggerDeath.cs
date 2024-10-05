using Godot;
using System;

public class TriggerDeath : ATrigger
{
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
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
