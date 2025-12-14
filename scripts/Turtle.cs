using Godot;
using System;

public partial class Turtle : ShootingEnemy
{
    protected override void DoAttack()
    {
        base.DoAttack();
        Bullet.Create(shootScene, Position, Vector2.Left);
    }
}
