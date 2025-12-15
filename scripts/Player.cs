using Godot;
using System;

public partial class Player : Entity
{
    [ExportCategory("Shooting")]
    [Export] private bool autoShoot;
    [Export] private float bulletSpace;

    private Bullet lastBullet;

    [Export] public PackedScene bulletScene;
    [Export] public PackedScene rocketScene;

    [Export] public Marker2D mouthPos;

    public int level;
    public int smartBombe;

    private static Player instance;

    public static Player GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null)
            instance = this;
        else QueueFree();
    }

    protected override void DoAction(float pDelta)
    {

        if (CanFire())
        {
            lastBullet = Bullet.Create(bulletScene, mouthPos.GlobalPosition, Vector2.Right);
            GameManager.GetInstance().AddChild(lastBullet);
            //Rocket.Create(rocketScene, this, Enemy.GetTarget(), mouthPos.GlobalPosition);
        }

        DoMove(pDelta);
    }

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        Vector2 lDirection = Input.GetVector("LEFT", "RIGHT", "UP", "DOWN").Normalized();

        Vector2 lScroll = Vector2.Right * GameManager.GetInstance().scrollSpeed;
        Vector2 lMove = (lDirection * speed + lScroll) * pDelta;

        Position += lMove;
    }


    private bool CanFire()
    {
        return Input.IsActionPressed("SHOOT") && (!IsInstanceValid(lastBullet) || lastBullet.Position.X - Position.X > bulletSpace);
    }
}
