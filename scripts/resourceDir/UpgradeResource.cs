using Godot;

namespace ggJAM228.scripts.resourceDir;

public partial class UpgradeResource : Resource
{
    [Export]
    public   float AddSpeed           = 0,
                   AddDamage          = 0,
                   AddMagicAmplifier  = 0,
                   AddMeleeAmplifier  = 0,
                   AddRangedAmplifier = 0,
                   AddRegeneration    = 0,
                   AddMaxHp           = 0,
                   AddShield          = 0,
                   AddScale           = 0;
    
}