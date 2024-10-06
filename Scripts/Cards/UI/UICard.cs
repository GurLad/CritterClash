using Godot;
using System;

public partial class UICard : Control
{
    [Export] private UICardRenderer Renderer;
    [ExportCategory("Colours")]
    [Export] private Color CantAffordModulate;
    [ExportCategory("Animation")]
    [Export] private float HoverTime = 0.2f;
    [Export] private float HoverSizeMod = 1f;
    [Export] private Vector2 HoverPositionMod = Vector2.Up * 20;

    public ACard Card { get; private set; }
    public bool Enemy { get; private set; } = false; // For now, single-player
    public int Index { get; set; }

    private Deck Deck;
    private Interpolator Interpolator = new Interpolator();

    private Vector2 BaseRendererPosition;
    private Vector2 BaseRendererScale;
    private Color BaseModulate;

    public void Init(Deck deck,bool enemy, int index, ACard card)
    {
        Deck = deck;
        Card = card;
        Enemy = enemy;
        Index = index;
        // Animation
        Interpolator.InterruptMode = Interpolator.Mode.Allowed;
        AddChild(Interpolator);
        BaseRendererPosition = Renderer.Position;
        BaseRendererScale = Renderer.Scale;
        BaseModulate = Renderer.Modulate;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        // Render
        Render();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (Deck.CanAffordCard(Index))
        {
            OnMouseExited();
            return this;
        }
        else
        {
            return base._GetDragData(atPosition);
        }
    }

    private void OnMouseEntered()
    {
        if (!Deck.CanAffordCard(Index))
        {
            return;
        }
        Interpolator.Interpolate(HoverTime,
            new Interpolator.InterpolateObject(
                a => Renderer.Position = a,
                Renderer.Position,
                BaseRendererPosition + HoverPositionMod,
                Easing.EaseInOutSin),
            new Interpolator.InterpolateObject(
                a => Renderer.Scale = a,
                Renderer.Scale,
                BaseRendererScale * HoverSizeMod,
                Easing.EaseInOutSin));
    }

    private void OnMouseExited()
    {
        if (!Deck.CanAffordCard(Index))
        {
            return;
        }
        Interpolator.Interpolate(HoverTime,
            new Interpolator.InterpolateObject(
                a => Renderer.Position = a,
                Renderer.Position,
                BaseRendererPosition,
                Easing.EaseInOutSin),
            new Interpolator.InterpolateObject(
                a => Renderer.Scale = a,
                Renderer.Scale,
                BaseRendererScale,
                Easing.EaseInOutSin));
    }

    public void Render()
    {
        Modulate = Deck.CanAffordCard(Index) ? BaseModulate : CantAffordModulate;
        Renderer.Render(Card);
    }
}
