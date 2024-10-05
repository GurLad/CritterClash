using Godot;
using System;
using System.Collections.Generic;

public class TriggerBeginTurn : ATrigger
{
    public TriggerBeginTurn(List<AEffect> effects) : base(effects) { }

    public TriggerBeginTurn(AEffect effect) : base(effect) { }

    protected override void ConnectInternal(Body body)
    {
        body.OnBeginTurn += BeginTurn;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnBeginTurn -= BeginTurn;
    }

    private void BeginTurn(TriggerParameter<Body> @this)
    {
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
