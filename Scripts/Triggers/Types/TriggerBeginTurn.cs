using Godot;
using System;

public class TriggerBeginTurn : ATrigger
{
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
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
