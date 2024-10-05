using Godot;
using System;
using System.Collections.Generic;

public partial class UIHand : Control
{
    [Export] private Container CardHolder;
    [Export] private PackedScene SceneUICard;

    private Deck Deck;
    private List<UICard> Hand { get; } = new List<UICard>();

    public void Init(Deck deck)
    {
        Deck = deck;
        Deck.OnCardPlaced += OnCardPlaced;
        Deck.OnCardDrawn += OnCardDrawn;
    }

    private void OnCardPlaced(int index, ACard card)
    {
        UICard removedCard = Hand[index];
        if (removedCard.Card != card)
        {
            GD.PrintErr("[UIHand]: Deck-UI desync!");
        }
        removedCard.QueueFree();
        Hand.ForEach((a, i) => a.Index = i);
    }

    private void OnCardDrawn(ACard card)
    {
        UICard newCard = SceneUICard.Instantiate<UICard>();
        CardHolder.AddChild(newCard);
        newCard.Index = Hand.Count;
        Hand.Add(newCard);
    }
}
