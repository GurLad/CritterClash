using Godot;
using System;

public partial class CardBody : ACard<BodyRecord>
{
    public override int Cost => Value.Cost;
    public override string Description => Value.Description;
    public override string FlavourText => Value.FlavourText;
    public override Texture2D CardIcon => Value.Texture;
    public override DisplayStats Stats => Value.BaseStats.ToDisplayStats();

    public CardBody(BodyRecord value) : base(value) { }

    public CardBody(string name) : base(BodyLoader.Get(name)) { }

    public override bool CanPlaceAt(bool enemy, GameGrid grid, Vector2I pos)
    {
        return grid.CanPlaceNewCritter(enemy, pos, Value);
    }

    public override void PlaceAt(bool enemy, GameGrid grid, Vector2I pos)
    {
        grid.PlaceNewCritter(enemy, pos, Value);
    }
}
