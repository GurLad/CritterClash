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
    public bool Enemy { get; set; } // Whatevs
    private List<ACard> Library { get; set; } = new List<ACard>();
    public List<ACard> Hand { get; } = new List<ACard>(); // Whatevs
    private List<ACard> Discard { get; } = new List<ACard>();

    public event Action OnBeginTurn;
    public event Action<ACard> OnCardDrawn;
    public event Action<int, ACard> OnCardPlaced;
    public event Action<int, ACard> OnCardDiscarded;

    private List<(int Count, ACard Card)> CardsBack;

    public Deck(bool enemy, List<(int Count, ACard Card)> cards)
    {
        Enemy = enemy;
        CardsBack = cards;
        cards.ForEach(a =>
        {
            for (int i = 0; i < a.Count; i++)
            {
                Library.Add(a.Card);
            }
        });
        Library = Library.Shuffle();
        DrawHand();
        Mana = 0;
    }

    public Deck(bool enemy, params (int Count, ACard Card)[] cards) : this(enemy, cards.ToList()) { }

    public void BeginTurn()
    {
        Mana = Mathf.Min(Mana + MANA_PER_TURN, MAX_MANA);
        for (int i = 0; i < Hand.Count; i++)
        {
            // TBA: Freezing...
            DiscardCard(i);
            i--;
        }
        DrawHand();
        OnBeginTurn?.Invoke();
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

    public bool CanAffordCard(int index)
    {
        if (index >= Hand.Count || index < 0)
        {
            GD.PrintErr("[Deck]: Playing a card outside the hand!");
            return false;
        }
        return Hand[index].Cost <= Mana;
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
    }

    private void DiscardCard(int index)
    {
        if (index >= Hand.Count || index < 0)
        {
            GD.PrintErr("[Deck]: Discarding a card outside the hand!");
            return;
        }
        ACard card = Hand[index];
        Discard.Add(card);
        Hand.RemoveAt(index);
        OnCardDiscarded?.Invoke(index, card);
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
            ShuffleDiscardToLibrary();
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
        targetIndex = DrawForcedCard<CardBodyPart>() ?? targetIndex;
        targetIndex = DrawForcedCard<CardBody>() ?? targetIndex;
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

    private int? DrawForcedCard<T>() where T : ACard
    {
        if (Hand.Count == HAND_SIZE - 1 && Hand.FindIndex(a => a is T) < 0)
        {
            int targetIndex = Library.FindIndex(a => a is T);
            if (targetIndex < 0)
            {
                ShuffleDiscardToLibrary();
                targetIndex = Library.FindIndex(a => a is T);
            }
            if (targetIndex < 0)
            {
                GD.PrintRaw("[Deck]: Zero " + typeof(T) + " in library & discard!");
                targetIndex = 0;
            }
            return targetIndex;
        }
        return null;
    }

    public Deck Clone()
    {
        return new Deck(Enemy, CardsBack);
    }
}
