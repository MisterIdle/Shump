using Godot;
using System;

public partial class PopCorn : Enemy
{
    public override void Initialize()
    {
        base.Initialize();
        velocity = DirectionTo(Player.GetInstance());
    }
}