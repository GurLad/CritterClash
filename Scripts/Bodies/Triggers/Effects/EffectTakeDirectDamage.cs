using Godot;
using System;
using System.Collections.Generic;

public class EffectTakeDirectDamage : AEffect
{
    private string Target { get; init; }
    private int Amount { get; init; }

    public EffectTakeDirectDamage(string target, int amount)
    {
        Target = target;
        Amount = amount;
    }

    public override bool Activate(Dictionary<string, TriggerParameter> args)
    {
        if (args.ContainsKey(Target))
        {
            Body body = args[Target].Get<Body>();
            if (body != null)
            {
                body.TakeDirectDamage(Amount);
                return true;
            }
            GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Not a body!");
            return false;
        }
        GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Doesn't exist!");
        return false;
    }
}
