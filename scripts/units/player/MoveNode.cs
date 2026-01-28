using Godot;
using System;

public partial class MoveNode : Node2D
{
	Unit unit;
	[Export] AnimatedSprite2D sprite;
	Camera2D camera;
	public override void _Ready()
	{
		unit = GetParent<Unit>();
		sprite.Play();
		camera = unit.GetNode<Camera2D>("Camera2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 vec = Input.GetVector("a", "d", "w", "s");
		vec = vec.Normalized();
		switch (vec.X)
		{
			case > 0:
				unit.Skew = 0.314f;
				unit.Rotation = -0.314f;
				break;
			case < 0:
				unit.Skew = -0.314f;
				unit.Rotation = 0.314f;
				break;
			default:
				unit.Skew = 0;
				unit.Rotation = 0;
				break;
		}
		if (vec == Vector2.Zero)
			sprite.Animation =  "idle";
		else
		{
			if (vec.Y >= 0)
				sprite.Animation = "walk";
			else
			{
				unit.Skew *= -1;
				unit.Rotation *= -1;
				sprite.Animation = "back";
			}
		}
		
		unit.Velocity = vec * unit.CurrentSpeed;
		unit.MoveAndSlide();
	}
}
