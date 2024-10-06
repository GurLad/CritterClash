using Godot;
using System;

public record LevelData(string Name, string Description, Deck humanDeck, Deck cpuDeck)
{
}
