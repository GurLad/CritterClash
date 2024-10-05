using Godot;
using System;
using System.Collections.Generic;

public class EffectModifyDamage : AEffect
{
    private string Target { get; init; }
    private Func<int, int> Formula { get; init; }

    public EffectModifyDamage(string target, Func<int, int> formula)
    {
        Target = target;
        Formula = formula;
    }

    public override bool Activate(Dictionary<string, TriggerParameter> args)
    {
        if (args.ContainsKey(Target))
        {
            TriggerParameter<int> param = args[Target].As<int>();
            if (param != null)
            {
                param.Data = Formula(param.Data);
                return true;
            }
            GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Not a body!");
            return false;
        }
        GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Doesn't exist!");
        return false;
    }
}
