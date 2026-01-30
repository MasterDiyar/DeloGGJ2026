using Godot;
using System;

public partial class BowAnimation : Sprite2D
{
    [Export]AnimationPlayer animationPlayer;
    [Export] private Timer _timer;

    public override void _Ready()
    {
        GetParent<Weapon>().Fire += (a) =>
        {
            animationPlayer.Play("RESET");
            animationPlayer.Play("natyazhenie");
        };
    }
}
