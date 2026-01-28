using Godot;
using System;

public partial class BowAnimation : Sprite2D
{
    [Export]AnimationPlayer animationPlayer;
    [Export] private Timer _timer;

    public override void _Ready()
    {
        _timer.Timeout += () => animationPlayer.Play("natyazhenie");
        GetParent<Weapon>().Fire += (a) => animationPlayer.Play("RESET");
    }
}
