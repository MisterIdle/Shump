using Godot;

public partial class Ammo : Movable
{
    [Export] private int lifeTime = 5;

    public override void Initialize()
    {
        base.Initialize();
        gameManager.gameContainer.AddChild(this);

        Timer lTimer = new Timer();
        lTimer.WaitTime = lifeTime;
        lTimer.Autostart = true;
        lTimer.Timeout += QueueFree;
        AddChild(lTimer);
    }

    protected override void OnCollide(Area2D pArea)
    {
        QueueFree();

        if (pArea is Entity pEntity)
        {
            pEntity.TakeDamage();
        }
    }
}
