using Godot;
using System;
using ggJAM228.scripts.resourceDir;

public partial class FirstMap : Node2D
{
	public UpgradeResource mobAmplifier = new UpgradeResource();

	public float XP=45;


	public override void _Ready()
	{
		mobAmplifier.isConcatenating += IsConcatenating;
	}

	private void IsConcatenating()
	{
		foreach (var nid in GetTree().GetNodesInGroup("enemy"))
			if (nid is Unit unit)
				unit.AddResource(mobAmplifier);
	}
}
