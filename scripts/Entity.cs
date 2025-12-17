using Godot;
using System;

public partial class Entity : Movable
{
    [Export] public int health = 1;
    [Export] public bool invulnerability;

    protected Vector2 DirectionTo(Entity pEntity)
    {
        return (pEntity.GlobalPosition - GlobalPosition).Normalized();
    }

    protected float DistanceTo(Entity pEntity)
    {
        return (pEntity.GlobalPosition - GlobalPosition).Length();
    }


    protected void SetInvulnerability(bool pBool)
    {
        invulnerability = pBool;
    }

    public void TakeDamage()
    {
        if (invulnerability) return;

        health -= 1;

        if (health <= 0)
            Death();
    }

    protected virtual void Death()
    {
        QueueFree();
    }

    protected override void OnCollide(Area2D pArea)
    {
        if (pArea is Entity lEntity && !lEntity.invulnerability)
        {
            lEntity.TakeDamage();
            TakeDamage();
        }
    }
}
