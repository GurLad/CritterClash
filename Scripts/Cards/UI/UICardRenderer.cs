using Godot;
using System;

public partial class UICardRenderer : Control
{
    [Export] private Label CostLabel;
    [Export] private Label NameLabel;
    [Export] private Label DescriptionLabel;
    [Export] private Container DescriptionContainer;
    [Export] private Label FlavourTextLabel;
    [Export] private TextureRect IconRect;
    [Export] private UIStats UIStats;
    [Export] private Container UIStatsContainer;

    public void Render(ACard card)
    {
        CostLabel.Text = card.Cost.ToString();
        NameLabel.Text = card.Name;
        DescriptionLabel.Text = card.Description;
        DescriptionContainer.Visible = !string.IsNullOrEmpty(card.Description);
        FlavourTextLabel.Text = card.FlavourText;
        IconRect.Texture = card.CardIcon;
        UIStatsContainer.Visible = UIStats.Render(card.Stats);
    }
}
