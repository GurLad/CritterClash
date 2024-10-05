using Godot;
using System;

public partial class UICardPlacementGrid : Control
{
    [Export] private GameGrid GameGrid;

    private Deck Deck;

    public void Init(Deck deck)
    {
        Deck = deck;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICard card)
        {
            return card.Card.CanPlaceAt(card.Enemy, GameGrid, atPosition.ToTile(GameGrid.Position));
        }
        else
        {
            return false;
        }
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.As<GodotObject>() is UICard card)
        {
            Deck.PlayCard(card.Index, GameGrid, atPosition.ToTile(GameGrid.Position));
        }
        else
        {
            GD.PrintErr("[UICardPlacementGrid]: Placing non-cards!");
        }
    }
}
