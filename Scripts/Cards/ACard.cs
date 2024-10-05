using Godot;
using System;

public abstract class ACard
{
    public abstract int Cost { get; }
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string FlavourText { get; }
    public abstract Texture2D CardIcon { get; }
    public abstract DisplayStats Stats { get; }

    public abstract bool CanPlaceAt(bool enemy, GameGrid grid, Vector2I pos);
    public abstract void PlaceAt(bool enemy, GameGrid grid, Vector2I pos);
}

public abstract class ACard<T> : ACard where T : ILoadedType
{
    protected T Value;

    public override string Name => Value.Name;

    protected ACard(T value)
    {
        Value = value;
    }
}
