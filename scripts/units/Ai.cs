using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Ai : Node2D
{
    private Unit unit;
    private List<Weapon> weapons;
    private Node2D BalBal, Player;

    private float vector;
    [Export] private float zoom = 128;
    bool Attacking = false;
    public override void _Ready()
    {
        unit = GetParent<Unit>();
        Player = (Node2D)GetTree().GetFirstNodeInGroup("player");

        weapons = unit.GetChildren().OfType<Weapon>().ToList();

    }

    public override void _PhysicsProcess(double delta)
    {
        Move((float)delta);
        
        if ( !Attacking)
            unit.MoveAndSlide();
        else
            foreach (var w in weapons)
                w.Attack(vector);
    }

    void Move(float delta)
    {
        if (!IsInstanceValid(Player)) { QueueFree();
            return; }
        Vector2 direction = (Player.GlobalPosition - unit.GlobalPosition).Normalized();
    
        vector = direction.Angle();
        unit.Velocity = unit.CurrentSpeed * Vector2.FromAngle(vector);
        
        Attacking = unit.Position.DistanceSquaredTo(Player.Position) <= zoom * zoom;

        unit.Velocity = !Attacking ? direction * unit.CurrentSpeed : Vector2.Zero;
    }
}
