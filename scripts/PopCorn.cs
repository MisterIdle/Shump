using Godot;
using System;

public partial class PopCorn : Enemy
{
    private PopCorn targetPopCorn;

    private bool isDying = false;

    protected override void Death()
    {
        if (isDying) return;
        isDying = true;

        targetPopCorn = (PopCorn)GetNearestEnemy();

        GD.Print("je suis actif");

        if (targetPopCorn != null)
        {
            Vector2 lDirection = DirectionTo(targetPopCorn);
            velocity = lDirection;
        }
        else
        {
            QueueFree();
        }
    }

    protected override void OnCollide(Area2D pArea)
    {
        base.OnCollide(pArea);

        if (pArea == targetPopCorn && !isDying)
        {
            targetPopCorn.Death();
            QueueFree();
        }
    }
}
