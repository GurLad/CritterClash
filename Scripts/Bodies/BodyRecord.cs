using Godot;
using System;
using System.Collections.Generic;

public record BodyRecord(List<(BodyPartType Type, Vector2I Position, bool Foreground)> PartSlots, Stats BaseStats, List<ATrigger> BaseTriggers)
{

}
