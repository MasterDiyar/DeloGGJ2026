using Godot;

namespace ggJAM228.scripts.resourceDir;

[GlobalClass]
public partial class MobsResource : Resource
{
    [Export] PackedScene UnitPrefab;
    [Export] Vector2 UnitPosition;
}