using Godot;
using System;

public partial class Bullet : Ammo
{
    public static Bullet Create(PackedScene pScene, Entity pEntity, Vector2 pPos, Vector2 pWay)
    {
        Bullet lBullet = (Bullet)pScene.Instantiate();

        lBullet.shooter = pEntity;
        lBullet.Position = pPos;

        lBullet.velocity = pWay;
        
        lBullet.Initialize();

        return lBullet;
    }
}