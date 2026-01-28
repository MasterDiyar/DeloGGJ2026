using System;
using Godot;
using ggJAM228.scripts.resourceDir;
using Timer = Godot.Timer;

public partial class Weapon : Node2D
{
	[Export] public int weaponType = 0; //melee, magic, ranged
	[Export] public BulletResource[] bulletScene =[];
	[Export] public float baseDamage = 10f;
	public bool canFire = true;
	public float multiplier = 1f;
	Timer fireTimer;
	public Action<float> Fire;
	public Unit myUnit;
	public override void _Ready()
	{
		myUnit = GetParent() as Unit;
		fireTimer = GetNode<Timer>("Timer");
		fireTimer.Timeout += Timeout;
		multiplier = (weaponType) switch {
			1 => myUnit.MeleeAmplifier,
			2 => myUnit.MagicAmplifier,
			3 => myUnit.RangedAmplifier,
			_ => 0
		};
	}

	protected virtual void Timeout()
	{
		canFire = true;
		fireTimer.Stop();
	}

	public virtual void Attack(float angle)
	{ 
		if (canFire) {
			canFire = false;
			fireTimer.Start();
			Fire?.Invoke(angle);
		}
	}
	
}
