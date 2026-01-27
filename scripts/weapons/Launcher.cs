using Godot;

namespace ggJAM228.scripts.weapons;

public partial class Launcher : Node2D
{
    [Export]protected Weapon myWeapon;
    [Export]protected int launchCount = 1;
    public override void _Ready()
    {
        myWeapon ??= GetParent<Weapon>();
        myWeapon.Fire += Launch;
    }

    public virtual void Launch(float angle)
    {
        
    }
}