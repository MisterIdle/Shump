using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : Entity
{
    private static List<Enemy> enemyList = new List<Enemy>();
    [Export] protected float playerDistance;
    private bool targeted;

    protected Vector2 playerDirection;

    public override void Initialize()
    {
        base.Initialize();

        enemyList.Add(this);
        playerDirection = DirectionTo(Player.GetInstance());
    }

    protected Entity GetNearestEnemy()
    {
        Enemy lNearest = null;
        float lMinDistance = float.MaxValue;

        foreach (Enemy lEnemy in enemyList)
        {
            if (lEnemy == this)
                continue;

            float lDistance = DistanceTo(lEnemy);

            if (lDistance < lMinDistance)
            {
                lMinDistance = lDistance;
                lNearest = lEnemy;
            }
        }

        return lNearest;
    }


    protected virtual void DetectPlayer() { }
}
