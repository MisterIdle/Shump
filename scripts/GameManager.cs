using Godot;
using System;

public partial class GameManager : Node2D
{
    [Export] public Node2D gameContainer;
    [Export] public Node2D baseContainer;

    private static GameManager instance;

    [Export] public float scrollSpeed;

    public static GameManager GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null) instance = this;
        else QueueFree();

        GD.Print(Name + " is ready");

        foreach (GameObject lGameObject in baseContainer.GetChildren())
        {
            lGameObject.Initialize();
        }
    }
}
