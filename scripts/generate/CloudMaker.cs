using Godot;
using System;
using System.Collections.Generic;

public partial class CloudMaker : Node2D
{
	[Export]
	Texture2D texture;
	[Export] Vector2I texturesCount = Vector2I.One;
	[Export] private int count = 10;
	[Export] public float Speed = 60f;

	private Timer timer;
	private List<Sprite2D> sprites = [];
	RandomNumberGenerator rng = new();
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Timeout += TimerOnTimeout;
		timer.Start();
		rng.Randomize();
		float h = texture.GetHeight()/ texturesCount.X, 
			  w = texture.GetWidth() / texturesCount.Y;

		for (int i = 0; i < count; i++){
			var sprite = new Sprite2D();
			sprite.Texture = texture;
			sprite.RegionEnabled = true;
			
			int col = rng.RandiRange(0, texturesCount.X - 1);
			int row = rng.RandiRange(0, texturesCount.Y - 1);
			sprite.RegionRect = new Rect2(col * w, row * h, w, h);
			
			sprite.Position = Vector2.Down * rng.RandiRange(0, 2048)+Vector2.Left*rng.RandiRange(0, 512);
			sprite.Rotation = rng.RandfRange(0, 6.28f);
			sprite.Scale = Vector2.One * rng.RandfRange(0.4f, 2.9f);
			
			AddChild(sprite);
			sprites.Add(sprite);
		}
	}

	public override void _Process(double delta)
	{
		float moveStep = Speed * (float)delta;
		foreach (var sprite in sprites)
			sprite.Position += new Vector2(moveStep, 0) * sprite.Scale;
	}

	private void TimerOnTimeout()
	{
		foreach (var sprite in sprites)
		{
			if (sprite.Position.X > 3000)
				sprite.Position += Vector2.Left*3200;
		}
	}
}
