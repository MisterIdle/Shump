using Godot;
using System;

public partial class Trigger : Movable
{
    private static Trigger instance;

    public static Trigger GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null)
            instance = this;
        else QueueFree();
    }

    public override void Initialize()
    {
        velocity = Vector2.Right;
        speed = GameManager.GetInstance().scrollSpeed;
        base.Initialize();
    }

    protected override void OnCollide(Area2D pArea)
    {
        base.OnCollide(pArea);

        if (pArea is GameObject lGameObject && !lGameObject.enable)
            lGameObject.Initialize();
    }
}
