using System;
using Godot;
using ggJAM228.scripts.resourceDir;
using ggJAM228.scripts.weapons;
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
	Launcher launcher;
	public override void _Ready()
	{
		launcher = GetNode<Launcher>("Launcher");
		myUnit = GetParent() as Unit;
		fireTimer = GetNode<Timer>("Timer");
		fireTimer.Timeout += Timeout;
		if (myUnit != null)
		{
			myUnit.UpdateWeapons += Update;
			Update();
		}

	}

	void Update()
	{
		multiplier = (weaponType) switch {
        	1 => myUnit.modifiers.AddMeleeAmplifier,
        	2 => myUnit.modifiers.AddMagicAmplifier,
        	3 => myUnit.modifiers.AddRangedAmplifier,
        	_ => 0
        };

		if (weaponType > 1)
		{
			foreach (var res in bulletScene)
			{
				res.LaunchCount += myUnit.modifiers.AddProjectile;
			}
		}
		launcher.
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
