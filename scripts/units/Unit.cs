using Godot;
using System;

public partial class Unit : CharacterBody2D
{
	[Export] public float
		UnitSpeed = 100f,
		MaxHp = 100f,
		Damage = 100f,
		MagicAmplifier = 100f,
		MeleeAmplifier = 100f,
		RangedAmplifier = 100f,
		Defence = 100f;

	public float Hp;
		
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TakeDamage(float damage)
	{
		if (Defence > 0)
			Defence -= damage;
		else
			Hp -= damage;
		if (Hp <= 0)
			DeferredDeath();

	}

	public virtual void DeferredDeath()
	{
		
	}
}
