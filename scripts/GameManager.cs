using Godot;
using System;

public partial class GameManager : Node2D
{
    [Export] public Node2D gameContainer;
    [Export] public Node2D baseContainer;
    [Export] public Node2D bulletContainer;

    [Export] public float scrollSpeed;

    public Vector2 screenSize;

    private static GameManager instance;

    public static GameManager GetInstance() => instance;

    public override void _Ready()
    {
        if (instance == null) instance = this;
        else QueueFree();

        foreach (Node lObject in baseContainer.GetChildren())
        {
            if (lObject is Player lPlayer)
                lPlayer.Initialize();

            if (lObject is Trigger lCamera)
                lCamera.Initialize();
        }
    }

    public override void _Process(double pDelta)
    {
        float lDelta = (float)pDelta;

        screenSize = GetViewportRect().Size + Trigger.GetInstance().Position;
    }
}
