using Godot;
using System;
using System.Collections.Generic;

public enum BodyPartType { Head, Leg, Arm, EndMarker }

public record BodyPart(BodyPartType Type, AStatsMod StatsMod, List<ATrigger> Triggers)
{

}
