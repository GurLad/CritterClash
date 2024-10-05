using Godot;
using System;

public partial class UICard : Control
{
    [Export] private Label NameField;
    [Export] private TextureRect IconRect;

    public ACard Card { get; private set; }
    public bool Enemy { get; private set; } = false; // For now, single-player
    public int Index { get; set; }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        return this;
    }
}
