using Godot;

public partial class Bullet : Ammo
{
    public static Bullet Create(PackedScene pScene, Entity pEntity, Vector2 pPos, Vector2 pWay, float pAngle = 0)
    {
        Bullet lBullet = (Bullet)pScene.Instantiate();

        lBullet.shooter = pEntity;
        lBullet.Position = pPos;

        lBullet.SetInitialMovement(pAngle, pWay);
        lBullet.Initialize();

        return lBullet;
    }

    private void SetInitialMovement(float pAngle, Vector2 pWay)
    {
        rand.Randomize();

        float spreadAngle = rand.RandfRange(-pAngle, pAngle);
        float finalAngle = pWay.Angle() + spreadAngle;
        velocity = Vector2.FromAngle(finalAngle) * pWay.Length();
        Rotation = finalAngle;
    }
}
