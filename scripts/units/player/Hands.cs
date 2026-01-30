using Godot;
using System;
using System.Linq;

public partial class Hands : CanvasLayer
{
	[Export] private Panel BowPanel, SwordPanel, GunPanel;
	
	private Unit player;
	[Export]private PackedScene[] weapons = [];

	[Export] public TextureProgressBar HpBar;
	[Export] public Line2D XpLine;
	public override void _Ready()
	{
		player = GetParent<Unit>();
		HpBar.MaxValue = player.CurrentMaxHp;
		HpBar.Value = player.Hp;
		BowPanel.GuiInput += (ev) => HandleCardInput(ev, BowPanel);
		SwordPanel.GuiInput += (ev) => HandleCardInput(ev, SwordPanel);
		GunPanel.GuiInput += (ev) => HandleCardInput(ev, GunPanel);
	}

	public int[] isUpgraded = [0, 0, 0];
	
	private void HandleCardInput(InputEvent @event, Panel card)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
		{
			switch (card.Name) {
				case "Bow": Addweapon(0+isUpgraded[0], card); break;
				case "Sword": Addweapon(1+isUpgraded[1], card); break;
				case "HandGun": Addweapon(2+isUpgraded[2], card); break;
			}
		}
	}

	public override void _Process(double delta)
	{HpBar.Value = player.Hp;
	}

	void Addweapon(int index, Panel panel)
	{
		if (index >= weapons.Length) return;

		foreach (var child in player.GetChildren())
			if (child is Weapon oldWep)
				oldWep.QueueFree();
		
		var newWep = weapons[index].Instantiate<Weapon>();
		player.AddChild(newWep);

		var textureRect = panel.GetChildOrNull<TextureRect>(0);
		if (textureRect != null)
		{
			var weaponSprite = newWep.GetChildren().OfType<Sprite2D>().FirstOrDefault();
			if (weaponSprite != null)
				textureRect.Texture = weaponSprite.Texture;
		}
	}
	
}
