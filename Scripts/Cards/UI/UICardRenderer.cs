using Godot;
using System;

public partial class UICardRenderer : Control
{
    [Export] private Label CostLabel;
    [Export] private Label NameLabel;
    [Export] private Label DescriptionLabel;
    [Export] private Label FlavourTextLabel;
    [Export] private TextureRect IconRect;
    [Export] private UIStats UIStats;

    public void Render(ACard card)
    {
        CostLabel.Text = card.Cost.ToString();
        NameLabel.Text = card.Name;
        DescriptionLabel.Text = card.Description;
        DescriptionLabel.Visible = !string.IsNullOrEmpty(card.Description);
        FlavourTextLabel.Text = card.FlavourText;
        IconRect.Texture = card.CardIcon;
        UIStats.Render(card.Stats);
    }
}
