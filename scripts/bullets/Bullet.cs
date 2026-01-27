using Godot;

namespace ggJAM228.scripts.bullets;

public partial class Bullet : Area2D
{
    
    [Export] public float Damage = 0;



    public virtual void OnHit(Area2D hit)
    {
        
    }
    
}