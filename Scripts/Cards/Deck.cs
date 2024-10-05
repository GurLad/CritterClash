using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    public const int HAND_SIZE = 5;
    private const int MANA_PER_TURN = 5;
    private const int MAX_MANA = 9;

    public int Mana { get; private set; }
    private bool Enemy { get; set; }
    private List<ACard> Library { get; set; } = new List<ACard>();
    private List<ACard> Hand { get; } = new List<ACard>();
    private List<ACard> Discard { get; } = new List<ACard>();

    public event Action<ACard> OnCardDrawn;
    public event Action<int, ACard> OnCardPlaced;

    public Deck(bool enemy, List<(int Count, ACard Card)> cards)
    {
        Enemy = enemy;
        cards.ForEach(a =>
        {
            for (int i = 0; i < a.Count; i++)
            {
                Library.Add(a.Card);
            }
        });
        Library = Library.Shuffle();
        DrawHand();
        Mana = MANA_PER_TURN;
    }

    public Deck(bool enemy, params (int Count, ACard Card)[] cards) : this(enemy, cards.ToList()) { }

    public void BeginTurn()
    {
        Mana = Mathf.Min(Mana + MANA_PER_TURN, MAX_MANA);
    }

    public bool CanPlayCard(int index, GameGrid grid, Vector2I position)
    {
        if (index >= Hand.Count || index < 0)
        {
            GD.PrintErr("[Deck]: Playing a card outside the hand!");
            return false;
        }
        return Hand[index].Cost <= Mana && Hand[index].CanPlaceAt(Enemy, grid, position);
    }

    public void PlayCard(int index, GameGrid grid, Vector2I position)
    {
        if (index >= Hand.Count || index < 0)
        {
            GD.PrintErr("[Deck]: Playing a card outside the hand!");
            return;
        }
        ACard card = Hand[index];
        if (card.Cost > Mana)
        {
            GD.PrintErr("[Deck]: Trying to play a card you cannot afford!");
        }
        Mana -= card.Cost;
        card.PlaceAt(Enemy, grid, position);
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
        int targetIndex = 0;
        if (Hand.Count == HAND_SIZE - 1 && Hand.FindIndex(a => a is CardBody) < 0)
        {
            targetIndex = Library.FindIndex(a => a is CardBody);
            if (targetIndex < 0)
            {
                ShuffleDiscardToLibrary();
                targetIndex = Library.FindIndex(a => a is CardBody);
            }
            if (targetIndex < 0)
            {
                GD.PrintRaw("[Deck]: Zero bodies in library & discard!");
                targetIndex = 0;
            }
        }
        ACard card = Library[targetIndex];
        Hand.Add(card);
        Library.RemoveAt(targetIndex);
        OnCardDrawn?.Invoke(card);
    }

    private void ShuffleDiscardToLibrary()
    {
        Discard.ForEach(a => Library.Add(a));
        Discard.Clear();
        Library = Library.Shuffle();
    }
}
