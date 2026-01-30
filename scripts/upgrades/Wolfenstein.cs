using Godot;
using System;

public partial class Wolfenstein : ChooseCard
{
	public override void _Ready()
	{
		base._Ready();
		GuiInput += HandleCardInput;
	}
	
	
	private void HandleCardInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
			((Hands)GetTree().GetFirstNodeInGroup("hands")).isUpgraded[0] = 2;
	}
}
