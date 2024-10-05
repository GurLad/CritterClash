using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    private const int HAND_SIZE = 5;

    private List<ACard> Library { get; set; } = new List<ACard>();
    private List<ACard> Hand { get; } = new List<ACard>();
    private List<ACard> Discard { get; } = new List<ACard>();

    public event Action<ACard> OnCardDrawn;
    public event Action<int, ACard> OnCardPlaced;

    public Deck(List<(int Count, ACard Card)> cards)
    {
        cards.ForEach(a =>
        {
            for (int i = 0; i < a.Count; i++)
            {
                Library.Add(a.Card);
            }
        });
        Library = Library.Shuffle();
        DrawHand();
    }

    public void PlayCard(int index)
    {
        if (index >= Hand.Count || index < 0)
        {
            GD.PrintErr("[Deck]: Playing a card outside the hand!");
        }
        ACard card = Hand[index];
        Discard.Add(card);
        Hand.RemoveAt(index);
        OnCardPlaced?.Invoke(index, card);
        DrawCard();
    }

    private void DrawHand()
    {
        if (Hand.Count > 0)
        {
            GD.PrintErr("[Deck]: Double draw hand!");
            return;
        }
        if (Library.Count < HAND_SIZE)
        {
            GD.PrintErr("[Deck]: Library is too small!");
            return;
        }
        while (Hand.Count < HAND_SIZE)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if (Library.Count <= 0)
        {
            ShuffleDiscardToLibrary();
        }
        if (Library.Count <= 0)
        {
            GD.PrintErr("[Deck]: Empty discard + library!");
            return;
        }
        ACard card = Library[0];
        Hand.Add(card);
        Library.RemoveAt(0);
        OnCardDrawn?.Invoke(card);
    }

    private void ShuffleDiscardToLibrary()
    {
        Discard.ForEach(a => Library.Add(a));
        Discard.Clear();
        Library = Library.Shuffle();
    }
}
