using Godot;

public partial class Entity : Movable
{
    [Export] public int health = 1;
    [Export] public bool invulnerability;

    [Export] protected float fallDeath = 20f;
    [Export] protected float deathJump = 5f;

    public bool isDeath = false;

    protected override void DoMove(float pDelta)
    {
        if (isDeath)
        {
            velocity.Y += fallDeath * pDelta;
        }

        base.DoMove(pDelta);
    }

    protected Vector2 DirectionTo(Entity pEntity) 
    { 
        return (pEntity.GlobalPosition - GlobalPosition).Normalized(); 
    }

    protected float DistanceTo(Entity pEntity) 
    { 
        return (pEntity.GlobalPosition - GlobalPosition).Length(); 
    }

    public void Fall()
    {
        if (!isDeath)
            return;

        velocity.Y = -deathJump;
    }

    public void TakeDamage()
    {
        if (invulnerability)
            return;

        health--;

        if (health <= 0)
            Death();
    }

    protected virtual void Death()
    {
        velocity = Vector2.Zero;
        isDeath = true;
        invulnerability = true;
        Fall();
    }

    protected override void OnCollide(Area2D pArea)
    {
        if (pArea is Enemy lEnemy && !lEnemy.invulnerability && !lEnemy.isDeath)
        {
            lEnemy.TakeDamage();
            TakeDamage();
        }

    }
}
