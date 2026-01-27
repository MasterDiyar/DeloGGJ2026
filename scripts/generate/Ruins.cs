using Godot;
using System;
using ggJAM228.scripts.resourceDir;

public partial class Ruins : Sprite2D
{
	[Export] private Label gradeLabel;
	[Export] private int cost = 15;
	[Export] private Area2D area;
	private bool canBuy = false;
	[Export] private PackedScene weaponScene;
	private Weapon wep;
	private int level = 1;
	private FirstMap map;
	[Export] public UpgradeResource info = new()
	{
		AddDamage = 10,
		AddMaxHp = 750
	};
	public override void _Ready()
	{
		area.BodyEntered += Inf;
		area.BodyExited += Ind;
		map = (FirstMap)GetTree().GetFirstNodeInGroup("map");
	}

	void Inf(Node2D body)
	{
		if (body.IsInGroup("player")) {
			gradeLabel.Visible = true;
			if (map.XP < cost) {
				canBuy = true;
				gradeLabel.LabelSettings.FontColor = Colors.Red; 
			}else
				gradeLabel.LabelSettings.FontColor = Colors.White;
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
		{
			
			Upgrade();
			
		}
	}

	void Upgrade()
	{
		level++;
		switch (level)
		{
			case 2:
				wep = weaponScene.Instantiate<Weapon>();
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
