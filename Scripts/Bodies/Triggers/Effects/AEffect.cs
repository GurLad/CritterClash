using Godot;
using System;
using System.Collections.Generic;

public abstract class AEffect
{
    public abstract bool Activate(Dictionary<string, TriggerParameter> args);
}
