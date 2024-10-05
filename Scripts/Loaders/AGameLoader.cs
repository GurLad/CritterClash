using Godot;
using System;
using System.Collections.Generic;

public abstract partial class AGameLoader<ThisType, LoadedType> : Node2D where ThisType : AGameLoader<ThisType, LoadedType> where LoadedType : ILoadedType
{
    private static ThisType Instance { get; set; }

    public abstract List<LoadedType> Records { get; }

    public override void _Ready()
    {
        base._Ready();
        Instance = (ThisType)this;
    }

    public static LoadedType Get(string name)
    {
        LoadedType result = Instance.Records.Find(a => a.Name == name);
        if (result == null)
        {
            GD.PrintErr("[AGameLoader]: " + name + " not found!");
        }
        return result;
    }
}
