using Godot;
using System;
using System.Collections.Generic;

public enum BodyPartType { Head, Leg, Arm, EndMarker }

public record BodyPartRecord(BodyPartType Type, int cost, string Name, AStatsMod StatsMod, List<ATrigger> Triggers, string Description, string FlavourText)
{

}
