using Godot;
using System;

public partial class CardBodyPart : ACard<BodyPartRecord>
{
    public override int Cost => Value.Cost;
    public override string Description => Value.Description;
    public override string FlavourText => Value.FlavourText;
    public override Texture2D CardIcon => Value.Sprite.Texture;
    public override DisplayStats Stats => Value.StatsMod.ToDisplayStats();

    public CardBodyPart(BodyPartRecord value) : base(value) { }

    public CardBodyPart(string name) : base(BodyPartLoader.Get(name)) { }

    public override bool CanPlaceAt(bool enemy, GameGrid grid, Vector2I pos)
    {
        return grid.CanAttachBodyPart(enemy, pos, Value);
    }

    public override void PlaceAt(bool enemy, GameGrid grid, Vector2I pos)
    {
        grid.AttachBodyPart(enemy, pos, Value);
    }
}
