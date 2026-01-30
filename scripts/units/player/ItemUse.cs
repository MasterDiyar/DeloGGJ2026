using Godot;

namespace ggJAM228.scripts.units.player;

public partial class ItemUse : Node2D
{
    [Export] private Unit myUnit;
    private Weapon _currentWeapon;

    public override void _Process(double delta)
    {
        if (!IsInstanceValid(_currentWeapon)) {
            _currentWeapon = null;
            foreach (Node child in myUnit.GetChildren()) {
                if (child is Weapon wp) {
                    _currentWeapon = wp;
                    break;
                }
            }
        }

        if (_currentWeapon != null)
        {
            var angle = GetAngleTo(GetGlobalMousePosition());
            _currentWeapon.Rotation = angle;
            
            bool shouldFlip = Mathf.Abs(angle) > Mathf.Pi / 2;
    
            Vector2 currentScale = _currentWeapon.Scale;
            _currentWeapon.Scale = new Vector2(currentScale.X, shouldFlip ? -1 : 1);
            
            if (Input.IsActionJustPressed("lm"))
                _currentWeapon.Attack(angle);
        }
    }
}