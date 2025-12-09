using Godot;
using System;

public partial class Player : Entity
{
    [ExportCategory("Shooting")]
    [Export] private bool autoShoot;
    [Export] private float shotSpace = 175;

    [Export] public PackedScene bulletScene;
    [Export] public PackedScene rocketScene;

    [Export] public Marker2D mouthPos;

    protected override void DoAction(float pDelta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            Rocket.Create(rocketScene, this, this, mouthPos.GlobalPosition);
            //Bullet.Create(bulletScene, this, mouthPos.GlobalPosition, Vector2.Right);
        }

        DoMove(pDelta);
    }

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        Vector2 lDirection = Input.GetVector("LEFT", "RIGHT", "UP", "DOWN");
        velocity = lDirection;

    }
}
