using Godot;

namespace ggJAM228.scripts.units.player;

public partial class ItemUse : Node2D
{
    [Export] private Unit myUnit;
    Weapon currentWeapon;
    public override void _Ready()
    {
        foreach (Node child in myUnit.GetChildren())
            if (child is Weapon wp)
                currentWeapon = wp;
    }

    public override void _Process(double delta)
    {
        var angle = GetAngleTo(GetGlobalMousePosition());
        currentWeapon.Rotation = angle;
        if (Input.IsActionJustPressed("lm"))
            currentWeapon.Attack(angle);
    }
}