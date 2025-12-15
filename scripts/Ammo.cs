using Godot;

public partial class Ammo : Movable
{
    [Export] private int lifeTime = 5;

    protected override void OnCollide(Area2D pArea)
    {
        if (pArea is Entity pEntity)
            pEntity.TakeDamage();
    }
}
