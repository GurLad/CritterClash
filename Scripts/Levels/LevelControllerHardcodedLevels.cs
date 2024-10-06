using Godot;
using System;
using System.Collections.Generic;

public partial class LevelController : Node
{
    private static List<LevelData> Levels { get; } = new List<LevelData>()
    {
        new LevelData(
            "Debug",
            "Debug",
            new Deck(false,
                (20, new CardBody("Ringabod")),
                (8, new CardBodyPart("Beholder")),
                (4, new CardBodyPart("Monkey Paw")),
                (4, new CardBodyPart("Tentacle")),
                (5, new CardBodyPart("Chicken Leg")),
                (3, new CardBodyPart("Wheel")),
                (1, new CardBodyPart("Cockatrice"))),
            new Deck(true,
                (20, new CardBody("Ringabod")),
                (8, new CardBodyPart("Beholder")),
                (4, new CardBodyPart("Monkey Paw")),
                (4, new CardBodyPart("Tentacle")),
                (5, new CardBodyPart("Chicken Leg")),
                (3, new CardBodyPart("Wheel")),
                (1, new CardBodyPart("Cockatrice"))))
    };
}
