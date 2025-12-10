using Godot;
using System;

public partial class GameManager : Node2D
{
    [Export] public Node2D gameContainer;

    private static GameManager instance;
    public static GameManager GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null) instance = this;
        else QueueFree();

        GD.Print(Name + " is ready");

        EnableInScreen();
    }

    private void EnableInScreen()
    {
        foreach (GameObject lGO in gameContainer.GetChildren())
        {
            GD.Print(lGO.Name);
            lGO.Initialize();
        }
    }
}
