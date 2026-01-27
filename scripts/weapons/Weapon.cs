using System;
using Godot;
using ggJAM228.scripts.resourceDir;
using Timer = Godot.Timer;

public partial class Weapon : Node2D
{
	[Export] BulletResource[] bullet_scene =[];
	[Export] protected float baseDamage = 10f;
	public bool canFire = true;
	Timer fireTimer;
	public Action<float> Fire;
	public override void _Ready()
	{
		fireTimer = GetNode<Timer>("Timer");
		fireTimer.Timeout += Timeout;
	}

	protected virtual void Timeout()
	{
		canFire = true;
	}

	protected virtual void Attack(float angle) =>
		Fire?.Invoke(angle);
	
}
