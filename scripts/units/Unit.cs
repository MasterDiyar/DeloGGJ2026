using Godot;
using System;
using ggJAM228.scripts.resourceDir;

public partial class Unit : CharacterBody2D
{
	[Export] public float
		UnitSpeed = 100f,
		MaxHp = 100f,
		Damage = 10f,
		MagicAmplifier = 1f,
		MeleeAmplifier = 1f,
		RangedAmplifier = 1f,
		Defence = 100f,
		XpOnDrop = 1f,
		Regeneration = 0;

	public float Hp;

	public void TakeDamage(float damage)
	{
		if (Defence > 0)
			Defence -= damage;
		else
			Hp -= damage;
		if (Hp <= 0)
			DeferredDeath();
	}

	public override void _Process(double delta)
	{
		Hp +=(Hp < MaxHp) ? (float)delta * Regeneration : 0;
	}

	public virtual void DeferredDeath()
	{
		var map = GetTree().GetFirstNodeInGroup("map") as FirstMap;
		map.XP += XpOnDrop;
		CallDeferred("queue_free");
	}

	public void AddResource(UpgradeResource resource)
	{
		UnitSpeed += resource.AddSpeed;
		MaxHp += resource.AddMaxHp;
		Damage += resource.AddDamage;
		Defence += resource.AddSpeed;
		MagicAmplifier += resource.AddMagicAmplifier;
		MeleeAmplifier += resource.AddMeleeAmplifier;
		RangedAmplifier += resource.AddRangedAmplifier;
		Regeneration += resource.AddRegeneration;
		Scale += Vector2.One * resource.AddScale;
	}
}
