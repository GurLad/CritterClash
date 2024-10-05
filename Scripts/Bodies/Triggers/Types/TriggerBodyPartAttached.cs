using Godot;
using System;
using System.Collections.Generic;

public class TriggerBodyPartAttached : ATrigger
{
    public TriggerBodyPartAttached(List<AEffect> effects) : base(effects) { }

    public TriggerBodyPartAttached(AEffect effect) : base(effect) { }

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
        Trigger(new Dictionary<string, TriggerParameter>()
        {
            { "this", @this },
            { "bodyPart", bodyPart },
        });
    }
}
