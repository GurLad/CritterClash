using Godot;
using System;

public class TriggerEndTurn : ATrigger
{
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
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this }
        });
    }
}
