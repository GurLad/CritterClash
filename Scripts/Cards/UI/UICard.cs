using Godot;
using System;

public partial class UICard : Control
{
    [Export] private UICardRenderer Renderer;

    public ACard Card { get; private set; }
    public bool Enemy { get; private set; } = false; // For now, single-player
    public int Index { get; set; }

    public void Init(bool enemy, int index, ACard card)
    {
        Card = card;
        Enemy = enemy;
        Index = index;
        Render(card);
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        return this;
    }

    private void Render(ACard card)
    {
        Renderer.Render(card);
    }
}
