using Godot;
using System;
using System.Collections.Generic;

public partial class UICardPlacementGrid : Control
{
    [Export] private GameGrid GameGrid;
    [ExportCategory("Tile Placement Previews")]
    [Export] private PackedScene GridPlacementPreviewTile;
    [Export] private Vector2 PreviewTilePosOffset;
    [Export] private Vector2 PreviewTileSizeOffset;

    private Deck Deck;
    private Dictionary<Vector2I, Control> PreviewTiles { get; } = new Dictionary<Vector2I, Control>();
    private Vector2I? HighlightedTile;

    public void Init(Deck deck)
    {
        Deck = deck;
        for (int x = 0; x < GameGrid.Size.X; x++)
        {
            for (int y = 0; y < GameGrid.Size.Y; y++)
            {
                Control previewTile = GridPlacementPreviewTile.Instantiate<Control>();
                AddChild(previewTile);
                previewTile.Size = ExtensionMethods.TILE_PHYSICAL_SIZE * Vector2.One + PreviewTileSizeOffset;
                previewTile.Position = new Vector2I(x, y).ToPhysicalLocation() + GameGrid.Position - previewTile.Size / 2 + PreviewTilePosOffset;
                previewTile.Visible = false;
                PreviewTiles.Add(new Vector2I(x, y), previewTile);
            }
        }
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (HighlightedTile != null)
        {
            PreviewTiles[HighlightedTile ?? throw new Exception("Impossible")].Visible = false;
            HighlightedTile = null;
        }
        if (data.As<GodotObject>() is UICard card)
        {
            if (Deck.CanPlayCard(card.Index, GameGrid, atPosition.ToTile(-GameGrid.Position)))
            {
                PreviewTiles[(HighlightedTile = atPosition.ToTile(-GameGrid.Position)) ?? throw new Exception("Impossible")].Visible = true;
                return true;
            }
        }
        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (HighlightedTile != null)
        {
            PreviewTiles[HighlightedTile ?? throw new Exception("Impossible")].Visible = false;
            HighlightedTile = null;
        }
        if (data.As<GodotObject>() is UICard card)
        {
            Deck.PlayCard(card.Index, GameGrid, atPosition.ToTile(-GameGrid.Position));
        }
        else
        {
            GD.PrintErr("[UICardPlacementGrid]: Placing non-cards!");
        }
    }
}
