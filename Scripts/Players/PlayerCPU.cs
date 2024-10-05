using Godot;
using System;

public partial class PlayerCPU : APlayerController
{
    protected override void BeginTurnInternal()
    {
        // Do random stuff
        int x = Enemy ? GameGrid.Size.X - 1 : 0;
        for (int y = 0; y < GameGrid.Size.Y; y++)
        {
            for (int j = 0; j < Deck.HAND_SIZE; j++)
            {
                if (Deck.CanPlayCard(j, GameGrid, new Vector2I(x, y)))
                {
                    Deck.PlayCard(j, GameGrid, new Vector2I(x, y));
                    j = 0;
                }
            }
        }
        EmitFinishTurn();
    }

    protected override void ConnectDeckInternal(Deck deck)
    {
        // Meh
    }
}
