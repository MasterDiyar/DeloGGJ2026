using System.Linq;
using Godot;

namespace ggJAM228.scripts.bullets;

public partial class Bullet : Area2D
{
    [Export] public float Damage = 0;
    [Export] public float Speed = 0;
    [Export] public float LifeTime = 5;

    public override void _Ready()
    {
        BodyEntered += OnHit;
    }


    public virtual void OnHit(Node2D hit)
    {
        if (hit is Unit unit) {
            GD.Print("Hit");
            if (unit.IsInGroup(GetGroups().FirstOrDefault())) return;
            unit.TakeDamage(Damage);
            LifeTime-=3;
        }
    }

    public override void _Process(double delta)
    {
        Position += Speed * Vector2.FromAngle(Rotation) * (float)delta;
        LifeTime -= (float)delta;
        if (LifeTime <= 0) QueueFree();
    }
}