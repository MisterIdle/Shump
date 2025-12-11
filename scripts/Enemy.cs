using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : Entity
{
    private static List<Enemy> enemyList = new List<Enemy>();
    [Export] private float playerDistance;
    private bool targeted;

    public override void Initialize()
    {
        enemyList.Add(this);
        velocity = Vector2.Left;

        base.Initialize();
    }

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        if (player.GlobalPosition.DistanceTo(Position) < playerDistance)
        {
            DetectPlayer();
        }
    }

    public static Enemy GetTarget()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (!enemyList[i].targeted)
            {
                enemyList[i].targeted = false;
                return enemyList[i];
            }
        }
        return null;
    }

    protected virtual void DetectPlayer() { }
}
