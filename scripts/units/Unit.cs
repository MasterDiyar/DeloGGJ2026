using Godot;
using System;
using System.Collections.Generic;
using ggJAM228.scripts.resourceDir;

public partial class Unit : CharacterBody2D
{
	[ExportGroup("Base Stats")]
	[Export] public float BaseSpeed = 100f, BaseMaxHp = 100f, BaseDamage = 10f, 
		BaseXpOnDrop = 1f, BaseRegeneration = 0f, BaseDefence = 100f;
	
	public UpgradeResource modifiers = new()
	{
		AddMeleeAmplifier = 1,
		AddMagicAmplifier = 1,
		AddRangedAmplifier = 1,
	};
	
	public float Hp, Armor, CurrentMaxHp, CurrentDamage, CurrentSpeed, CurrentXpOnDrop, CurrentRegeneration, CurrentMaxArmor;
	
	private bool _isMovingUp = false;
	private List<Sprite2D> _masks = [];

	public Action UpdateWeapons;


	public override void _Ready()
	{
		if (!IsInGroup("player"))
			AddResource(((FirstMap)GetTree().GetFirstNodeInGroup("map")).mobAmplifier);
		foreach (var nid in GetChildren())
			if (nid.IsInGroup("mask") && nid is Sprite2D sprite)
				_masks.Add(sprite);
		
		UpdateFinalStats();
		Hp = CurrentMaxHp;
		Armor = CurrentMaxArmor;
	}

	public void TakeDamage(float damage)
	{
		if (Armor > 0)
			Armor -= damage;
		else
			Hp -= damage;
		GD.Print(Armor, " <-armor, hp-> " ,Hp);
		if (Hp <= 0)
			DeferredDeath();
	}
	
	public void UpdateFinalStats()
	{
		CurrentMaxHp = BaseMaxHp + modifiers.AddMaxHp;
		CurrentDamage = BaseDamage + modifiers.AddDamage;
		CurrentSpeed = BaseSpeed + modifiers.AddSpeed;
		CurrentXpOnDrop = BaseXpOnDrop + modifiers.AddXP;
		CurrentRegeneration = BaseRegeneration + modifiers.AddRegeneration;
		CurrentMaxArmor = BaseDefence + modifiers.AddShield;
        
		Scale = Vector2.One * (1.0f + modifiers.AddScale);
        
		if (Hp > CurrentMaxHp) Hp = CurrentMaxHp;
		if (Armor > CurrentMaxArmor) Armor = CurrentMaxArmor;
		
		UpdateWeapons?.Invoke();
	}
	
	public void AddResource(UpgradeResource resource)
	{
		modifiers.Concat(resource); 
		UpdateFinalStats();      
		foreach (var nid in GetChildren())
			if (nid.IsInGroup("mask") && nid is Sprite2D sprite)
				_masks.Add(sprite);
	}

	public override void _Process(double delta)
	{
		HandleZIndex();
		if (Hp < CurrentMaxHp)
			Hp += (float)delta * CurrentRegeneration;
	}

	private void HandleZIndex()
	{
		if (Velocity.Y == 0) return;
		bool movingUp = Velocity.Y < 0;
        
		if (movingUp != _isMovingUp) {
			_isMovingUp = movingUp;
			int newZ = _isMovingUp ? -1 : 0;
            
			foreach (var mask in _masks)
				mask.ZIndex = newZ;
		}
	}

	public virtual void DeferredDeath()
	{
		var map = GetTree().GetFirstNodeInGroup("map") as FirstMap;
		map.XP += CurrentXpOnDrop;
		CallDeferred("queue_free");
	}
}
