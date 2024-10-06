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
    [ExportCategory("Colours")]
    [Export] private Color FlavourColour;
    [Export] private Color DescriptionColour;

    public void Render(ACard card)
    {
        CostLabel.Text = card.Cost.ToString();
        NameLabel.Text = card.Name;
        FlavourTextLabel.Text = !string.IsNullOrEmpty(card.Description) ? card.Description : card.FlavourText;
        FlavourTextLabel.SelfModulate = (!string.IsNullOrEmpty(card.Description) ? DescriptionColour : FlavourColour);
        //DescriptionContainer.Visible = !string.IsNullOrEmpty(card.Description);
        //FlavourTextLabel.Text = card.FlavourText;
        IconRect.Texture = card.CardIcon;
        UIStatsContainer.Visible = UIStats.Render(card.Stats);
    }
}
