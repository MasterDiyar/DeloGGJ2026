using Godot;
using System;

public partial class LeopardAi : Ai
{
	AnimatedSprite2D as2D;
	public override void _Ready()
	{
		base._Ready();
		as2D = GetNode<AnimatedSprite2D>("../AnimatedSprite2D");
		as2D.Play();
	}

	public override void _PhysicsProcess(double delta)
	{
		Move((float)delta);
        
		if ( !Attacking) {
			if (!as2D.IsPlaying()) as2D.Play();
			unit.MoveAndSlide();
		}else {
			if (as2D.IsPlaying()) as2D.Pause();
			foreach (var w in weapons)
				w.Attack(vector);
		}
		
		unit.Rotation = vector;
		
		if (Mathf.Abs(vector) > 0.1f) 
			as2D.FlipH = Mathf.Cos(vector) < 0; 
		
	}
}
