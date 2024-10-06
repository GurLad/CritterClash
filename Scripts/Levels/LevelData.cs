using Godot;
using System;

public record LevelData(string Name, string Description, Deck HumanDeck, Deck CpuDeck)
{
}
