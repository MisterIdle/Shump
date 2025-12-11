using Godot;
using System;

public partial class Coin : Collectible
{
    protected override void OnCollide(Area2D pArea)
    {
        GD.Print("coin");
        base.OnCollide(pArea);
    }
}
