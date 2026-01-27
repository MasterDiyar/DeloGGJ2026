using Godot;

namespace ggJAM228.scripts.resourceDir;

public partial class BulletResource : Resource
{
    [Export] public float BaseDamage = 1;
    [Export] public int LaunchCount = 1;
    [Export] public PackedScene bullet_scene;
}