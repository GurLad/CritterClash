using Godot;
using System;
using System.Collections.Generic;

public abstract class AEffect
{
    public abstract void Activate(Dictionary<string, TriggerParameter> args);
}
