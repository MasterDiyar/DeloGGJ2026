using Godot;
using System;

public partial class MoveNode : Node2D
{
	Unit unit;

	public override void _Ready()
	{
		unit = GetParent<Unit>();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 vec = Input.GetVector("a", "d", "w", "s");
		vec = vec.Normalized();
		unit.Velocity = vec * unit.UnitSpeed;
		unit.MoveAndSlide();
	}
}
