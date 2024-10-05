using Godot;
using System;

public class TriggerParameter
{
    public T Get<T>()
    {
        if (this is TriggerParameter<T> t)
        {
            return t.Data;
        }
        else
        {
            GD.PrintErr("[TriggerParameter]: Wrong type! Expecting " + typeof(T) + ", got " + GetType());
            return default;
        }
    }

    public TriggerParameter<T> As<T>()
    {
        if (this is TriggerParameter<T> t)
        {
            return t;
        }
        else
        {
            GD.PrintErr("[TriggerParameter]: Wrong type! Expecting " + typeof(T) + ", got " + GetType());
            return default;
        }
    }
}

public class TriggerParameter<T> : TriggerParameter
{
    public T Data { get; set; }

    public TriggerParameter(T data)
    {
        Data = data;
    }
}
