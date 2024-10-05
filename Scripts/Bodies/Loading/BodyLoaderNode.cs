using Godot;
using System;

public partial class BodyLoaderNode : Sprite2D
{
    [Export] public Node2D[] Heads { get; set; }
    [Export] public Node2D[] Arms { get; set; }
    [Export] public Node2D[] Legs { get; set; }
}
