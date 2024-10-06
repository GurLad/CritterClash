using Godot;
using System;
using System.Collections.Generic;

public partial class VFXSFXController : Node
{
    public static VFXSFXController Instance { get; private set; }

    [Export] private Node[] Vfx;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public 
}
