using Godot;
using System;
using System.Collections.Generic;

public partial class VFXSFXController : Node
{
    public static VFXSFXController Instance { get; private set; }

    [Export] private PackedScene SceneHitVfx;
    [Export] private PackedScene SceneDamageText;

    [Export] private PackedScene SceneSpawnVfx;

    [Export] private AudioStream[] DamagedSFX;
    [Export] private AudioStream[] SpawnSFX;
    [Export] private AudioStream[] AttackSFX;
    [Export] private AudioStream[] DeadSFX;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
    }

    public void DisplayDamage(Vector2 pos, int amount)
    {
        CpuParticles2D hitVfx = SceneHitVfx.Instantiate<CpuParticles2D>();
        AddChild(hitVfx);
        hitVfx.Position = pos;
        hitVfx.Finished += () => hitVfx.QueueFree();
        hitVfx.Restart();
        DamageText damageText = SceneDamageText.Instantiate<DamageText>();
        AddChild(damageText);
        damageText.Display(amount, pos);
        damageText.Position = pos;
        Play(DamagedSFX.RandomItemInList());
    }

    public void DisplaySpawn(Vector2 pos)
    {
        GpuParticles2D hitVfx = SceneSpawnVfx.Instantiate<GpuParticles2D>();
        AddChild(hitVfx);
        hitVfx.Position = pos;
        hitVfx.Finished += () => hitVfx.QueueFree();
        hitVfx.Restart();
        Play(SpawnSFX.RandomItemInList());
    }

    public void PlayAttack()
    {
        Play(AttackSFX.RandomItemInList());
    }

    public void PlayDead()
    {
        Play(DeadSFX.RandomItemInList());
    }

    private void Play(AudioStream stream)
    {
        AudioStreamPlayer newPlayer = new AudioStreamPlayer();
        AddChild(newPlayer);
        newPlayer.Stream = stream;
        newPlayer.Finished += () => newPlayer.QueueFree();
        newPlayer.Play();
    }
}
