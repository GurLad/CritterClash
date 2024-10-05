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
            return card.Card.CanPlaceAt(card.Enemy, GameGrid, atPosition.ToTile());
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
            card.Card.PlaceAt(card.Enemy, GameGrid, atPosition.ToTile());
            Deck.PlayCard(card.Index);
        }
        else
        {
            GD.PrintErr("[UICardPlacementGrid]: Placing non-cards!");
        }
    }
}
