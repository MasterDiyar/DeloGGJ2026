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
    [Export] protected float Offset = 0;

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
                for (var k = 0; k < myWeapon.bulletScene[j].LaunchCount; k++)
                    CreateBullet(myWeapon.bulletScene[j].bullet_scene,
                        starter + i * betweenAngle + k * betweenAngle + j * betweenAngle / 2,
                        myWeapon.baseDamage + myWeapon.bulletScene[j].BaseDamage);
            else foreach (var t in myWeapon.bulletScene)
                for (var k = 0; k < t.LaunchCount; k++)
                    CreateBullet(t.bullet_scene,
                        myWeapon.baseDamage + t.BaseDamage,
                        starter + i * betweenAngle + k * betweenAngle);
            
        }
    }

    public void CreateBullet(PackedScene bulletScene, float damage, float angle)
    {
        var bullet = bulletScene.Instantiate<Bullet>();
        bullet.Rotation = angle;
        bullet.Position = GetGlobalPosition() + Vector2.FromAngle(angle) * Offset;
        bullet.Damage += damage * myWeapon.multiplier;
        bullet.AddToGroup(GetParent().GetParent().GetGroups().FirstOrDefault());
        map.AddChild(bullet);
    }
}