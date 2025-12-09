using Godot;
using System;

public partial class Movable : GameObject
{
    [Export] public float speedX;
    [Export] public float speedY;

    public Vector2 velocity;
    public Vector2 speed;

    protected override void DoAction(float pDelta)
    {
        DoMove(pDelta);
    }

    protected virtual void DoMove(float pDelta) 
    {
        speed = new Vector2(speedX, speedY);
        Position += velocity * speed * pDelta;
    }
}
