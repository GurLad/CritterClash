using Godot;
using System;
using System.Collections.Generic;

public partial class LevelController : Node
{
    private static List<LevelData> Levels { get; } = new List<LevelData>()
    {
        new LevelData(
            "Tutorial 1",
            "Welcome to Critter Clash! It's a bit complex and I'm nearly out of time so please bear with me!\n" +
            "You are the gnome on the left, and your goal is to defeat the gnome on the right.\n" +
            "To do that, you need to create Critters - which eventually Clash - using your cards.\n" +
            "You get 5 cards and 5 mana per turn. Leftover cards will be discarded, but mana will remain for the next turn (up to 9).\n" +
            "To place a card, drag it into the play area - it will light up if it's a valid move. To end your turn, press the green arrow.\n" +
            "All Critters need a Body first - Bodies specify which parts they can hold, amongst Heads, Arms and Legs.\n" +
            "You can only place a Body on the left-most column - aka adjacent to your gnome. Keep this in mind!\n" +
            "After placing a Body, you can add Parts to it - Parts grant stat bonouses and/or effects. Parts can be placed anywhere (on your Critters).\n" +
            "For this level, there are no enemies - try placing a body and giving it legs to reach the enemy!",
            new Deck(false,
                (30, new CardBody("Mammal")),
                (10, new CardBodyPart("Cheetah")),
                (10, new CardBodyPart("Cat"))),
            new Deck(true,
                (40, new CardBodyPart("Gnome")))),
        new LevelData(
            "Tutorial 2",
            "Well done! So far, you only moved, but now let's get to the Clash part!\n" +
            "Critters have 3 stats: Health, Attack, Speed.\n" +
            "Health is the red heart - if it reachs 0, the critter dies.\n" +
            "Attack is the orange explosion - critters with 0 or less attack cannot, well, attack.\n" +
            "Speed is the blue circle - cirtters with 0 or less speed cannot move nor attack!\n" +
            "Critters will attack if they would otherwise stand on the tile of their opponent - similar to chess.\n" +
            "Also like chess, critters will take the spot of their opponent if they die.\n" +
            "Good luck!",
            new Deck(false,
                (20, new CardBody("Scorpion")),
                (20, new CardBody("Land Shark")),
                (5, new CardBodyPart("Cheetah")),
                (5, new CardBodyPart("Spiky Leg")),
                (5, new CardBodyPart("Tentacle")),
                (5, new CardBodyPart("Cursor")),
                (5, new CardBodyPart("Gnome")),
                (5, new CardBodyPart("Fly"))),
            new Deck(true,
                (30, new CardBody("Mammal")),
                (10, new CardBodyPart("Cheetah")),
                (10, new CardBodyPart("Spiky Leg")),
                (10, new CardBodyPart("Gnome")),
                (10, new CardBodyPart("Cat")))),
        new LevelData(
            "Level 1 - Growing Pains",
            "Let's spice things up a little - how about critters which slowly get stronger?",
            new Deck(false,
                (10, new CardBody("Scorpion")),
                (10, new CardBody("Land Shark")),
                (10, new CardBody("Mammal")),
                (10, new CardBody("Merworm")),
                (7, new CardBodyPart("Lsr Swd 3000")),
                (7, new CardBodyPart("T-Rex")),
                (7, new CardBodyPart("Spiky Arm")),
                (7, new CardBodyPart("Frog")),
                (7, new CardBodyPart("Wheel")),
                (7, new CardBodyPart("Peg Leg")),
                (7, new CardBodyPart("Brain")),
                (7, new CardBodyPart("Bulldog")),
                (7, new CardBodyPart("Mosquito"))),
            new Deck(true,
                (20, new CardBody("Scorpion")),
                (20, new CardBody("Land Shark")),
                (5, new CardBodyPart("Cheetah")),
                (5, new CardBodyPart("Spiky Leg")),
                (5, new CardBodyPart("Tentacle")),
                (5, new CardBodyPart("Cursor")),
                (5, new CardBodyPart("Gnome")),
                (5, new CardBodyPart("Fly")))),
        new LevelData(
            "Level 2 - Shrinking Gains",
            "How about the opposite this time - start strong, end up dead.",
            new Deck(false,
                (10, new CardBody("Scorpion")),
                (10, new CardBody("Land Shark")),
                (10, new CardBody("Mammal")),
                (10, new CardBody("Ninja Turtle")),
                (7, new CardBodyPart("Monkey Paw")),
                (7, new CardBodyPart("Bear Arm")),
                (7, new CardBodyPart("Skelly Arm")),
                (7, new CardBodyPart("Frog")),
                (7, new CardBodyPart("House Cat")),
                (7, new CardBodyPart("Skelly Leg")),
                (7, new CardBodyPart("Cat")),
                (7, new CardBodyPart("Woodpecker")),
                (7, new CardBodyPart("Fly"))),
            new Deck(true,
                (10, new CardBody("Scorpion")),
                (10, new CardBody("Land Shark")),
                (10, new CardBody("Mammal")),
                (10, new CardBody("Merworm")),
                (7, new CardBodyPart("Lsr Swd 3000")),
                (7, new CardBodyPart("T-Rex")),
                (7, new CardBodyPart("Spiky Arm")),
                (7, new CardBodyPart("Frog")),
                (7, new CardBodyPart("Wheel")),
                (7, new CardBodyPart("Peg Leg")),
                (7, new CardBodyPart("Brain")),
                (7, new CardBodyPart("Bulldog")),
                (7, new CardBodyPart("Mosquito")))),
        new LevelData(
            "Level 3 - Legless Day",
            "Who needs limbs anyway?",
            new Deck(false,
                (10, new CardBody("Snail")),
                (2, new CardBody("Angry Snail")),
                (10, new CardBody("Butterfly")),
                (2, new CardBody("Flutter Fly")),
                (10, new CardBody("Bee")),
                (7, new CardBodyPart("Cat")),
                (7, new CardBodyPart("Woodpecker")),
                (7, new CardBodyPart("Fly")),
                (7, new CardBodyPart("Bulldog")),
                (7, new CardBodyPart("Mosquito"))),
            new Deck(true,
                (10, new CardBody("Scorpion")),
                (10, new CardBody("Land Shark")),
                (10, new CardBody("Mammal")),
                (10, new CardBody("Ninja Turtle")),
                (7, new CardBodyPart("Monkey Paw")),
                (7, new CardBodyPart("Bear Arm")),
                (7, new CardBodyPart("Skelly Arm")),
                (7, new CardBodyPart("Frog")),
                (7, new CardBodyPart("House Cat")),
                (7, new CardBodyPart("Skelly Leg")),
                (7, new CardBodyPart("Cat")),
                (7, new CardBodyPart("Woodpecker")),
                (7, new CardBodyPart("Fly")))),
        new LevelData(
            "Level 4 - Chaos",
            "One copy of everything - go wild!",
            new Deck(false,
                (1, new CardBody("Ringabod")),
                (1, new CardBody("Snail")),
                (1, new CardBody("Angry Snail")),
                (1, new CardBody("Bee")),
                (1, new CardBody("Biped Blob")),
                (1, new CardBody("Butterfly")),
                (1, new CardBody("Flutter Fly")),
                (1, new CardBody("Scorpion")),
                (1, new CardBody("Dry Scorpion")),
                (1, new CardBody("Mammal")),
                (1, new CardBody("Land Shark")),
                (1, new CardBody("Sky Shark")),
                (1, new CardBody("Merworm")),
                (1, new CardBody("Turtle")),
                (1, new CardBody("Ninja Turtle")),
                (1, new CardBodyPart("Beholder")),
                (1, new CardBodyPart("Brain")),
                (1, new CardBodyPart("Alien")),
                (1, new CardBodyPart("Bulldog")),
                (1, new CardBodyPart("Fly")),
                (1, new CardBodyPart("Cat")),
                (1, new CardBodyPart("Gnome")),
                (1, new CardBodyPart("Mosquito")),
                (1, new CardBodyPart("Woodpecker")),
                (1, new CardBodyPart("Beholder")),
                (1, new CardBodyPart("Tentacle")),
                (1, new CardBodyPart("Bear Arm")),
                (1, new CardBodyPart("Cursor")),
                (1, new CardBodyPart("Skelly Arm")),
                (1, new CardBodyPart("Spiky Arm")),
                (1, new CardBodyPart("Lsr Swd 3000")),
                (1, new CardBodyPart("T-Rex")),
                (1, new CardBodyPart("Chicken Leg")),
                (1, new CardBodyPart("Cockatrice")),
                (1, new CardBodyPart("Wheel")),
                (1, new CardBodyPart("Frog")),
                (1, new CardBodyPart("House Cat")),
                (1, new CardBodyPart("Cheetah")),
                (1, new CardBodyPart("Peg Leg")),
                (1, new CardBodyPart("Skelly Leg")),
                (1, new CardBodyPart("Spiky Leg"))),
            new Deck(true,
                (1, new CardBody("Ringabod")),
                (1, new CardBody("Snail")),
                (1, new CardBody("Angry Snail")),
                (1, new CardBody("Bee")),
                (1, new CardBody("Biped Blob")),
                (1, new CardBody("Butterfly")),
                (1, new CardBody("Flutter Fly")),
                (1, new CardBody("Scorpion")),
                (1, new CardBody("Dry Scorpion")),
                (1, new CardBody("Mammal")),
                (1, new CardBody("Land Shark")),
                (1, new CardBody("Sky Shark")),
                (1, new CardBody("Merworm")),
                (1, new CardBody("Turtle")),
                (1, new CardBody("Ninja Turtle")),
                (1, new CardBodyPart("Beholder")),
                (1, new CardBodyPart("Brain")),
                (1, new CardBodyPart("Alien")),
                (1, new CardBodyPart("Bulldog")),
                (1, new CardBodyPart("Fly")),
                (1, new CardBodyPart("Cat")),
                (1, new CardBodyPart("Gnome")),
                (1, new CardBodyPart("Mosquito")),
                (1, new CardBodyPart("Woodpecker")),
                (1, new CardBodyPart("Beholder")),
                (1, new CardBodyPart("Tentacle")),
                (1, new CardBodyPart("Bear Arm")),
                (1, new CardBodyPart("Cursor")),
                (1, new CardBodyPart("Skelly Arm")),
                (1, new CardBodyPart("Spiky Arm")),
                (1, new CardBodyPart("Lsr Swd 3000")),
                (1, new CardBodyPart("T-Rex")),
                (1, new CardBodyPart("Chicken Leg")),
                (1, new CardBodyPart("Cockatrice")),
                (1, new CardBodyPart("Wheel")),
                (1, new CardBodyPart("Frog")),
                (1, new CardBodyPart("House Cat")),
                (1, new CardBodyPart("Peg Leg")),
                (1, new CardBodyPart("Skelly Leg")),
                (1, new CardBodyPart("Spiky Leg"))))
    };
}
