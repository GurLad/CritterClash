using Godot;
using System;
using System.Collections.Generic;

public abstract class ATrigger
{
    public List<AEffect> Effects;

    protected void Trigger(List<ATriggerParameter> args)
    {
        Effects.ForEach(a => a.Activate(args));
    }

    public abstract void Connect(Body body);
    public abstract void Disconnect(Body body);
}
