using Godot;
using System;

public partial class UIPlayerMana : Node
{
    [Export] private Label AmountDisplay;
    [Export] private APlayerController Player;

    private Deck Deck;

    public void Init(Deck deck)
    {
        Deck = deck;
        UpdateDisplay();
        deck.OnCardPlaced += OnCardPlaced;
        deck.OnBeginTurn += OnBeginTurn;
    }

    private void OnCardPlaced(int arg1, ACard arg2)
    {
        UpdateDisplay();
    }

    private void OnBeginTurn()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        AmountDisplay.Text = Deck.Mana.ToString();
    }
}
