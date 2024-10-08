using Godot;
using System;
using System.Collections.Generic;

public abstract class ATrigger
{
    public List<AEffect> Effects;

    protected ATrigger(List<AEffect> effects)
    {
        Effects = effects;
    }

    protected ATrigger(AEffect effect)
    {
        Effects = new List<AEffect>() { effect };
    }

    protected void Trigger(Dictionary<string, TriggerParameter> args)
    {
        Effects.ForEach(a => a.Activate(args));
    }

    public void Connect(Body body)
    {
        ConnectInternal(body);
    }

    public void Disconnect(Body body)
    {
        DisconnectInternal(body);
    }

    protected abstract void ConnectInternal(Body body);
    protected abstract void DisconnectInternal(Body body);
}
