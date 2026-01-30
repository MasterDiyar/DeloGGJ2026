using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using ggJAM228.scripts.resourceDir;

public partial class Ruins : Sprite2D
{
	[Export] private Texture2D aftergrade;
	[Export] private Label gradeLabel;
	[Export] private int cost = 15;
	[Export] private Area2D area, shootArea;
	private bool canBuy = false;
	[Export] private PackedScene weaponScene;
	private Weapon wep;
	private int level = 1;
	private FirstMap map;
	private List<Unit> fireUnits = [];
	bool Attacking = false;
	[Export] public UpgradeResource info = new()
	{
		AddDamage = 10,
		AddMaxHp = 750
	};
	public override void _Ready()
	{
		area.BodyEntered += Inf;
		area.BodyExited += Ind;
		shootArea.BodyEntered += Attack;
		shootArea.BodyExited += Remove;
		map = (FirstMap)GetTree().GetFirstNodeInGroup("map");
	}

	void Attack(Node2D node)
	{
		if (node is not Unit u || !u.IsInGroup("enemy")) return;
		fireUnits.Add(u);
		Attacking = true;
	}

	void Remove(Node2D node)
	{
		if (node is not Unit u ) return;
		fireUnits.Remove(u);
		Attacking = fireUnits.Count != 0;
	}

	void Inf(Node2D body)
	{
		if (body.IsInGroup("player")) {
			gradeLabel.Visible = true;
			canBuy = map.XP >= cost;
			gradeLabel.LabelSettings.FontColor = !canBuy ? Colors.Red : Colors.White;
		}
	}
	void Ind(Node2D body)
	{
		if (body.IsInGroup("player")) {
			canBuy = false;
			gradeLabel.Visible = false;
		}
	}

	public override void _Process(double delta)
	{
		if (canBuy &&  Input.IsActionJustPressed("f"))
			Upgrade();

		if (Attacking)
		{
			var angle = ( fireUnits.FirstOrDefault().GlobalPosition-GlobalPosition).Angle();
			wep.Attack(angle);
		}
	}

	void Upgrade()
	{
		level++;
		GD.Print(level);
		switch (level)
		{
			case 2:
				wep = weaponScene.Instantiate<Weapon>();
				AddChild(wep);
				Texture = aftergrade;
				gradeLabel.Text = "Теперь нужно 100xp";
				cost = 100;
				shootArea.Monitorable = true;
				shootArea.Monitoring = true;
				break;
			case 3:
				info.AddDamage = 15;
				break;
			case 4:
				info.AddDamage = 20;
				info.AddMaxHp += 50;
				break;
		}
	}
}
