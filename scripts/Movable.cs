using Godot;
using System;

public partial class Movable : GameObject
{
    [Export] public float speed;
    public Vector2 velocity;

    protected override void DoAction(float pDelta)
    {
        DoMove(pDelta);
    }

    protected virtual void DoMove(float pDelta) 
    {
        Position += velocity * speed * pDelta;
    }
}
