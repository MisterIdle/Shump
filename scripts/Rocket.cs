using Godot;
using System;

public partial class Rocket : Ammo
{
    private Entity target;

    public bool isActive;

    [Export] private float rotationSpeed = 10;

    public static Rocket Create(PackedScene pScene, Entity pTarget, Vector2 pPos)
    {
        Rocket lRocket = (Rocket)pScene.Instantiate();

        lRocket.Position = pPos;
        lRocket.target = pTarget;

        lRocket.Initialize();

        return lRocket;
    }

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        if (!isActive) return;

        Vector2 lDirection = (target.Position - Position).Normalized();
        float lTargetAngle = lDirection.Angle();
    
        Rotation = Mathf.LerpAngle(Rotation, lTargetAngle, rotationSpeed * pDelta);
        velocity = Vector2.FromAngle(Rotation);
    }

    protected override void OnCollide(Area2D pArea)
    {
        base.OnCollide(pArea);

        if (!isActive) return;
        QueueFree();
    }
}
