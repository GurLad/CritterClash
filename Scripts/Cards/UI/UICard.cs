using Godot;
using System;

public partial class UICard : Control
{
    [Export] private Label CostLabel;
    [Export] private Label NameLabel;
    [Export] private Label DescriptionLabel;
    [Export] private Label FlavourTextLabel;
    [Export] private TextureRect IconRect;
    [Export] private UIStats UIStats;

    public ACard Card { get; private set; }
    public bool Enemy { get; private set; } = false; // For now, single-player
    public int Index { get; set; }

    public void Init(bool enemy, int index, ACard card)
    {
        Card = card;

    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        return this;
    }

    private void Render(ACard card)
    {
        CostLabel.Text = card.Cost.ToString();
        NameLabel.Text = card.Name;
        DescriptionLabel.Text = card.Description;
        FlavourTextLabel.Text = card.FlavourText;
        IconRect.Texture = card.CardIcon;
        UIStats.Render(card.Stats);
    }
}
