using Godot;
using System;

public partial class Turtle : ShootingEnemy
{
    protected override void DoShoot()
    {
        base.DoShoot();
        Bullet.Create(shootScene, this, Position, Vector2.Left);
    }
}
