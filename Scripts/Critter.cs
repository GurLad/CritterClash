using Godot;
using System;
using System.Collections.Generic;

public partial class Critter : Node2D
{
    // Exports
    [ExportCategory("Nodes")]
    [Export] private UIStats UIStats;
    [Export] private Sprite2D Renderer;
    [ExportCategory("Colours")]
    [Export] private Color BaseModulate;
    [Export] private Color ActedModulate;
    [ExportCategory("Animation")]
    [Export] private float MoveTime = 0.5f;
    [Export] private float AttackDistance = 20;
    [Export] private float PreAttackTime = 0.3f;
    [Export] private float MidAttackPause = 0.2f;
    [Export] private float PostAttackTime = 0.5f;
    [Export] private float DamagedDistance = 20;
    [Export] private float PreDamagedTime = 0.3f;
    [Export] private float MidDamagedPause = 0.2f;
    [Export] private float PostDamagedTime = 0.5f;
    [Export] private float DieTime = 0.5f;
    [Export] private float DieRotationCount = 5;
    // Properties
    public Body Body { get; private set; }
    public bool Enemy { get; private set; }
    public int Direction => Enemy ? -1 : 1;
    private Vector2I _tile;
    public Vector2I Tile
    {
        get => _tile;
        set
        {
            if (_tile == value)
            {
                return;
            }
            _tile = value;
            AnimateMove(value);
        }
    }
    private bool _busy = false;
    public bool Busy => _busy || ActionQueue.Count > 0;
    public bool Acted { get; set; }

    private Dictionary<BodyPartType, List<Node2D>> BodyPartLocations { get; } = new Dictionary<BodyPartType, List<Node2D>>();
    private Queue<Action> ActionQueue { get; } = new Queue<Action>();
    private Interpolator Interpolator { get; } = new Interpolator();

    [Signal]
    public delegate void OnFinishAnimationEventHandler(Critter critter);

    [Signal]
    public delegate void OnBeginAnimationEventHandler(Critter critter);

    public override void _Ready()
    {
        base._Ready();
        AddChild(Interpolator);
    }

    public void Init(bool enemy, Vector2I tile, BodyRecord bodyRecord)
    {
        _tile = tile;
        Enemy = enemy;
        Position = Tile.ToPhysicalLocation();
        Renderer.FlipH = Enemy;
        Body = new Body(bodyRecord);
        for (int i = 0; i < (int)BodyPartType.EndMarker; i++)
        {
            BodyPartLocations.Add((BodyPartType)i, new List<Node2D>());
        }
        Renderer.Texture = Body.Record.Texture;
        Body.Record.PartSlots.ForEach(a =>
        {
            Node2D holder = new Node2D();
            AddChild(holder);
            holder.Position = a.Position;
            holder.ZIndex = a.Foreground ? 1 : -1;
            BodyPartLocations[a.Type].Add(holder);
        });
        Body.OnDealDamage += AnimateDealDamage;
        Body.OnTakeDamage += AnimateTakeDamage;
        Body.OnDeath += AnimateDeath;
        Render(); // Just render at a bunch of places...
    }

    public void BeginTurn()
    {
        Body.BeginTurn();
        Render(); // Just render at a bunch of places...
    }

    public void EndTurn()
    {
        Body.EndTurn();
        Render(); // Just render at a bunch of places...
    }

    public bool CanAttachPart(BodyPartRecord part)
    {
        return Body.CanAttachPart(part);
    }

    public void AttachPart(BodyPartRecord part)
    {
        Body.AttachPart(part);
        Node2D holder = BodyPartLocations[part.Type].RandomItemInList();
        BodyPartLocations[part.Type].Remove(holder);
        Sprite2D newSprite = (Sprite2D)part.Sprite.Duplicate();
        holder.AddChild(newSprite);
        holder.Position = new Vector2(holder.Position.X * Direction, holder.Position.Y);
        newSprite.Position = new Vector2(newSprite.Position.X * Direction, newSprite.Position.Y);
        newSprite.FlipH = Enemy;
        newSprite.Visible = true;
        UpdateModulate();
        Render(); // Just render at a bunch of places...
    }

    public void Attack(Critter target)
    {
        if (!PreAnimate(() => Attack(target))) return;

        Interpolator.Interpolate(PreAttackTime,
            new Interpolator.InterpolateObject(
                a => Position = a,
                Position,
                Position + new Vector2(Direction * AttackDistance, 0),
                Easing.EaseInQuad));
        Interpolator.OnFinish = () =>
        {
            Body.DealDamage(target.Body);
            target.Render(); // Just render at a bunch of places...
            PostAnimate();
        };
    }

    public void UpdateModulate()
    {
        Modulate = (Acted || Body.BaseStats.Speed <= 0) ? ActedModulate : BaseModulate;
    }

    private void AnimateMove(Vector2I target)
    {
        if (!PreAnimate(() => AnimateMove(target))) return;

        Interpolator.Interpolate(MoveTime,
            new Interpolator.InterpolateObject(
                a => Position = a,
                Position,
                Tile.ToPhysicalLocation(),
                Easing.EaseInOutQuad));
        Interpolator.OnFinish = PostAnimate;
    }

    private void AnimateDealDamage(TriggerParameter<Body> @this, TriggerParameter<Body> target, TriggerParameter<int> damage)
    {
        if (!PreAnimate(() => AnimateDealDamage(@this, target, damage))) return;

        if (Body.Dead)
        {
            PostAnimate();
        }
        Interpolator.Delay(MidAttackPause);
        Interpolator.OnFinish = () =>
        {
            Interpolator.Interpolate(PostAttackTime,
                new Interpolator.InterpolateObject(
                    a => Position = a,
                    Position,
                    Position - new Vector2(Direction * AttackDistance, 0),
                    Easing.EaseInOutQuad));
            if (target.Data.Dead && !Body.Dead)
            {
                Interpolator.OnFinish = () =>
                {
                    Tile = Tile + Vector2I.Right * Direction;
                    PostAnimate();
                };
            }
            else
            {
                Interpolator.OnFinish = PostAnimate;
            }
        };
    }

    private void AnimateTakeDamage(TriggerParameter<Body> @this, TriggerParameter<Body> attacker, TriggerParameter<int> damage)
    {
        if (!PreAnimate(() => AnimateTakeDamage(@this, attacker, damage))) return;

        Interpolator.Interpolate(PreDamagedTime,
            new Interpolator.InterpolateObject(
                a => Position = a,
                Position,
                Position - new Vector2(Direction * DamagedDistance, 0),
                Easing.EaseOutElastic));
        Interpolator.OnFinish = () =>
        {
            Interpolator.Delay(MidDamagedPause);
            if (!Body.Dead)
            {
                Interpolator.OnFinish = () =>
                {
                    Interpolator.Interpolate(PostDamagedTime,
                        new Interpolator.InterpolateObject(
                            a => Position = a,
                            Position,
                            Position + new Vector2(Direction * AttackDistance, 0),
                            Easing.EaseInOutQuad));
                    Interpolator.OnFinish = PostAnimate;
                };
            }
            else
            {
                PostAnimate();
            }
        };
    }

    private void AnimateDeath(TriggerParameter<Body> @this)
    {
        if (!PreAnimate(() => AnimateDeath(@this))) return;

        Interpolator.Interpolate(PreDamagedTime,
            new Interpolator.InterpolateObject(
                a => Scale = a,
                Scale,
                Vector2.One / 10000f),
            new Interpolator.InterpolateObject(
                a => Rotation = a,
                Rotation,
                Rotation + Mathf.Pi * 2 * DieRotationCount));
        Interpolator.OnFinish = () =>
        {
            QueueFree();
            PostAnimate();
        };
    }

    private bool PreAnimate(Action callback)
    {
        Render(); // Just render at a bunch of places...
        if (Busy)
        {
            ActionQueue.Enqueue(callback);
            return false;
        }
        _busy = true;
        EmitSignal(SignalName.OnBeginAnimation, this);
        return true;
    }

    private void PostAnimate()
    {
        Render(); // Just render at a bunch of places...
        if (ActionQueue.Count > 0)
        {
            _busy = false;
            ActionQueue.Dequeue().Invoke();
        }
        else
        {
            _busy = false;
            EmitSignal(SignalName.OnFinishAnimation, this);
        }
    }

    private void Render()
    {
        if (Body.Dead)
        {
            UIStats.Visible = false;
        }
        else
        {
            UIStats.Render(Body.Stats.ToDisplayStats());
        }
    }
}
