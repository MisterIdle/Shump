using Godot;
using System;

public partial class Turtle : ShootingEnemy
{
    protected override void DoShoot()
    {
        base.DoShoot();
        Rocket.Create(shootScene, player, Position);
    }
}
