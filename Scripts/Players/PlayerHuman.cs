using Godot;
using System;

public partial class PlayerHuman : APlayerController
{
    [Export] private UICardPlacementGrid PlacementGrid;
    [Export] private UIHand Hand;
    [Export] private Control PlayerUI;

    public void FinishTurn()
    {
        PlayerUI.Visible = false;
        EmitFinishTurn();
    }

    protected override void BeginTurnInternal()
    {
        // Animate....
        PlayerUI.Visible = true;
    }

    protected override void ConnectDeckInternal(Deck deck)
    {
        PlacementGrid.Init(deck);
        Hand.Init(deck);
    }
}
