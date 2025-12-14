using Godot;

public partial class ShootingEnemy : Enemy
{
    [Export] private float shootRate;
    [Export] protected float shootSpread;
    [Export] protected PackedScene shootScene;

    public override void Initialize()
    {
        base.Initialize();
        StartShooting();
    }

    private void StartShooting()
    {
        Timer lTimer = new Timer();

        lTimer.WaitTime = shootRate;
        lTimer.Autostart = true;
        lTimer.Timeout += DoAttack;

        AddChild(lTimer);
    }

    protected virtual void DoAttack() { }
}
