using Godot;
using System;

public partial class Collectible : Movable
{
    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);
        velocity = Vector2.Down;
    }

    protected override void OnCollide(Area2D pArea) => QueueFree();
}
