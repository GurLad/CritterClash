using Godot;
using System;

public class TriggerBodyPartAttached : ATrigger
{
    protected override void ConnectInternal(Body body)
    {
        body.OnBodyPartAttached += BodyPartAttached;
    }

    protected override void DisconnectInternal(Body body)
    {
        body.OnBodyPartAttached -= BodyPartAttached;
    }

    private void BodyPartAttached(TriggerParameter<Body> @this, TriggerParameter<BodyPartRecord> bodyPart)
    {
        Trigger(new System.Collections.Generic.Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "bodyPart", bodyPart },
        });
    }
}
