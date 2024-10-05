using Godot;
using System;
using System.Collections.Generic;

public class TriggerEndTurn : ATrigger
{
    public TriggerEndTurn(List<AEffect> effects) : base(effects) { }

    public TriggerEndTurn(AEffect effect) : base(effect) { }

    protected override void ConnectInternal(Body body)
    {
        body.OnEndTurn += EndTurn;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnEndTurn -= EndTurn;
    }

    private void EndTurn(TriggerParameter<Body> @this)
    {
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
