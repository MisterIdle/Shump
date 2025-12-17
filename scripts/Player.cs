using Godot;

public partial class Player : Entity
{
    [ExportCategory("Shooting")]
    [Export] private bool autoShoot;
    [Export] private float bulletSpace;

    [Export] private PackedScene bulletScene;
    [Export] private PackedScene rocketScene;
    [Export] private Marker2D mouthPos;
    [Export] private Hammer hammer;

    public int level;
    public int smartBombe;

    private Bullet lastBullet;

    private bool isDashing;
    private float dashTimeLeft;
    private float dashSpeed;

    static Player instance;
    public static Player GetInstance() => instance;

    public override void _Ready()
    {
        if (instance == null) instance = this;
        else QueueFree();

        hammer.Initialize();
    }

    protected override void DoAction(float delta)
    {
        hammer.Attack(delta);

        if (CanFire())
            Fire();

        DoMove(delta);
    }

    private void Fire()
    {
        lastBullet = Bullet.Create(bulletScene, mouthPos.GlobalPosition, Vector2.Right);
        GameManager.GetInstance().AddChild(lastBullet);
    }

    public void Dash(float pTime)
    {
        isDashing = true;
        dashTimeLeft = pTime;
        dashSpeed = speed * 3f;
    }

    protected override void DoMove(float delta)
    {
        Vector2 lDirection = Input.GetVector("LEFT", "RIGHT", "UP", "DOWN");
        Vector2 lScroll = Vector2.Right * GameManager.GetInstance().scrollSpeed;

        if (isDashing)
        {
            velocity = velocity.Lerp(Vector2.Right * dashSpeed, 10f * delta);
            dashTimeLeft -= delta;

            if (dashTimeLeft <= 0f)
                isDashing = false;
        }
        else
        {
            Vector2 lTargetVelocity = lDirection * speed + lScroll;
            velocity = velocity.Lerp(lTargetVelocity, 8f * delta);
        }

        Position += velocity * delta;
    }

    private bool CanFire()
    {
        if (!Input.IsActionPressed("SHOOT"))
            return false;

        if (!IsInstanceValid(lastBullet))
            return true;

        return lastBullet.Position.X - Position.X > bulletSpace;
    }
}
