using Godot;
using System;
using System.Collections.Generic;

public class EffectAddStatsMod : AEffect
{
    private string Target { get; init; }
    private AStatsMod Mod { get; init; }

    public EffectAddStatsMod(string target, AStatsMod mod)
    {
        Target = target;
        Mod = mod;
    }

    public override bool Activate(Dictionary<string, TriggerParameter> args)
    {
        if (args.ContainsKey(Target))
        {
            Body body = args[Target].Get<Body>();
            if (body != null)
            {
                body.AddStatsMod(Mod);
                return true;
            }
            GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Not a body!");
            return false;
        }
        GD.PrintErr("[EffectAddStatsMod]: Failed to add statmod to " + Target + "! Doesn't exist!");
        return false;
    }
}
