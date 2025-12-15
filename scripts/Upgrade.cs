using Godot;
using System;

public partial class Upgrade : Collectible
{
    protected override void OnCollide(Area2D pArea)
    {
        //player.level++;
        base.OnCollide(pArea);
    }
}
