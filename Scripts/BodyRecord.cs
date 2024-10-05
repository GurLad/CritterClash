using Godot;
using System;
using System.Collections.Generic;

public record BodyRecord(Dictionary<BodyPartType, int> PartSlots, Stats BaseStats, List<ATrigger> BaseTriggers)
{

}
