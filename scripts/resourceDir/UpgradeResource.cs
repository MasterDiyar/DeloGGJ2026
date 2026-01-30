using System;
using Godot;

namespace ggJAM228.scripts.resourceDir;

[GlobalClass]
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
                   AddScale           = 0,
                   AddDistance        = 0,
                   AddXP              = 0;

    [Export] public int AddProjectile = 0;

    public void Concat(UpgradeResource resource)
    {
        AddSpeed += resource.AddSpeed;
        AddDamage += resource.AddDamage;
        AddMagicAmplifier += resource.AddMagicAmplifier;
        AddMeleeAmplifier += resource.AddMeleeAmplifier;
        AddRangedAmplifier += resource.AddRangedAmplifier;
        AddRegeneration += resource.AddRegeneration;
        AddMaxHp += resource.AddMaxHp;
        AddShield += resource.AddShield;
        AddScale += resource.AddScale;
        AddDistance += resource.AddDistance;
        AddProjectile += resource.AddProjectile;
        AddXP += resource.AddXP;
        isConcatenating?.Invoke();
    }

    public Action isConcatenating;
}