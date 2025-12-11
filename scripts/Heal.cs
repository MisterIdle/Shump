using Godot;
using System;

public partial class Heal : Collectible
{
    protected override void OnCollide(Area2D pArea)
    {
        player.health++;
        base.OnCollide(pArea);
    }
}
