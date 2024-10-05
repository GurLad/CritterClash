using Godot;
using System;
using System.Collections.Generic;

public enum BodyPartType { Head, Leg, Arm, EndMarker }

public record BodyPartRecord(BodyPartType Type, int Cost, string Name, AStatsMod StatsMod, List<ATrigger> Triggers, string Description, string FlavourText)
    : ILoadedType
{
    public Sprite2D Sprite { get; private set; }
    public bool Inited { get; private set; }

    public void AttachSprite(Sprite2D sprite)
    {
        if (Inited)
        {
            GD.PrintErr("[BodyPartRecord]: Double init!");
        }
        Sprite = sprite;
        Inited = true;
    }
}
