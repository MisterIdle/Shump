using Godot;
using System.Net.Sockets;

public partial class Bullet : Ammo
{
    public static Bullet Create(PackedScene pScene, Vector2 pPos, Vector2 pWay, float pAngle = 0)
    {
        Bullet lBullet = (Bullet)pScene.Instantiate();

        lBullet.Position = pPos;

        lBullet.SetInitialMovement(pAngle, pWay);
        lBullet.Initialize();

        return lBullet;
    }

    private void SetInitialMovement(float pAngle, Vector2 pWay)
    {
        rand.Randomize();

        float lSpreadAngle = rand.RandfRange(-pAngle, pAngle);
        float lFinalAngle = pWay.Angle() + lSpreadAngle;
        velocity = Vector2.FromAngle(lFinalAngle) * pWay.Length();
        Rotation = lFinalAngle;
    }
}
