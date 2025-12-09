using Godot;
using System;

public partial class Rocket : Ammo
{
    private Entity target;

    public static Rocket Create(PackedScene pScene, Entity pEntity, Entity pTarget, Vector2 pPos)
    {
        Rocket lRocket = (Rocket)pScene.Instantiate();

        lRocket.shooter = pEntity;
        lRocket.Position = pPos;
        lRocket.target = pTarget;

        lRocket.Initialize();

        return lRocket;
    }

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        Vector2 direction = (target.Position - Position).Normalized();
        float targetAngle = direction.Angle();

        Rotation = Mathf.LerpAngle(Rotation, targetAngle, 10f * pDelta);
        velocity = Vector2.FromAngle(Rotation);

        GD.Print(target);
    }

}
