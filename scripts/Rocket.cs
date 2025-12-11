using Godot;
using System;

public partial class Rocket : Ammo
{
    private Entity target;

    private const int ROTATION_SPEED = 10;

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

        Vector2 lDirection = (target.Position - Position).Normalized();
        float lTargetAngle = lDirection.Angle();

        Rotation = Mathf.LerpAngle(Rotation, lTargetAngle, ROTATION_SPEED * pDelta);
        velocity = Vector2.FromAngle(Rotation);
    }

}
