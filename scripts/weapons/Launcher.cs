using System;
using System.Linq;
using ggJAM228.scripts.bullets;
using ggJAM228.scripts.resourceDir;
using Godot;

namespace ggJAM228.scripts.weapons;

public partial class Launcher : Node2D
{
    
    [Export]protected Weapon myWeapon;
    [Export]protected int launchCount = 1;
    [Export] protected bool perLaunch = false,
                            isCentered = false;
    [Export]protected float betweenAngle = 0;

    private FirstMap map;
    public override void _Ready()
    {
        map = (FirstMap)GetTree().GetFirstNodeInGroup("map");
        myWeapon ??= GetParent<Weapon>();
        myWeapon.Fire += Launch;
    }

    public virtual void Launch(float angle)
    {
        float starter = (isCentered) ? angle - betweenAngle * launchCount/2 : angle;
        for (int i = 0; i < launchCount; i++)
        {
            if (perLaunch) for (int j = 0; j < myWeapon.bulletScene.Length; j++)
                    CreateBullet(myWeapon.bulletScene[j].bullet_scene,
                        starter + i * betweenAngle + j * betweenAngle / 2,
                        myWeapon.baseDamage + myWeapon.bulletScene[j].BaseDamage);
            else foreach (var t in myWeapon.bulletScene)
                    CreateBullet(t.bullet_scene,
                        myWeapon.baseDamage + t.BaseDamage,
                        starter + i * betweenAngle);
            
        }
    }

    public void CreateBullet(PackedScene bulletScene, float damage, float angle)
    {
        var bullet = bulletScene.Instantiate<Bullet>();
        bullet.Rotation = angle;
        bullet.Position = GetGlobalPosition();
        bullet.Damage += damage * myWeapon.multiplier;
        bullet.AddToGroup(GetParent().GetParent().GetGroups().FirstOrDefault());
        map.AddChild(bullet);
    }
}