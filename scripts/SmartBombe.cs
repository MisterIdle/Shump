using Godot;
using System;

public partial class SmartBombe : Collectible
{
    protected override void OnCollide(Area2D pArea)
    {
        player.smartBombe++;
        base.OnCollide(pArea);
    }
}
