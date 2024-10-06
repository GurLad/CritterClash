using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerCPU : APlayerController
{
    protected override void BeginTurnInternal()
    {
        // Do random stuff
        int x = Enemy ? GameGrid.Size.X - 1 : 0;
        List<int> ys = new List<int>() { 0, 1, 2 };
        ys = ys.Shuffle();
        for (int y1 = 0; y1 < ys.Count; y1++)
        {
            int y = ys[y1];
            for (int j = 0; j < Deck.Hand.Count; j++)
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
