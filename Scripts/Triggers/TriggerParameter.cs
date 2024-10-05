using Godot;
using System;

public class TriggerParameter { }

public class TriggerParameter<T> : TriggerParameter
{
    public T Data { get; set; }

    public TriggerParameter(T data)
    {
        Data = data;
    }
}
