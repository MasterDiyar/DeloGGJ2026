using Godot;
using System;
using ggJAM228.scripts.resourceDir;

public partial class ChooseCard : Panel
{
	[Export] public UpgradeResource upgradeResource = new();
	
	[Export] public UpgradeResource downgradeResource = new();
}
