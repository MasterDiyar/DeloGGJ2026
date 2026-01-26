using Godot;
using System;

public partial class Planter : Node2D
{
	[Export] private Texture2D[] plants;
	[Export] private int count = 10;
	[Export] private Vector2 offset = new Vector2(0, 0);
	[Export] private Vector2 size = new Vector2(0, 0);
	[Export] private Vector2 plantSize = new Vector2(0, 1);
	private RandomNumberGenerator rng;
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		rng.Randomize();

		for (int i = 0; i < count; i++)
		{
			Vector2 pos = new Vector2(rng.RandfRange(0, size.X), rng.RandfRange(0, size.Y));
			Seed(plants[rng.RandiRange(0, plants.Length-1)], pos+offset, rng.RandfRange(plantSize.X, plantSize.Y) );
		}
	}

	void Seed(Texture2D texture, Vector2 position, float scale)
	{
		var sprite = new Sprite2D();
		sprite.Texture = texture;
		sprite.Position = position;
		sprite.Scale = Vector2.One * scale;
		CallDeferred("add_child",sprite);
	}
}
