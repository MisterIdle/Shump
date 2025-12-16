using Godot;
using System;

public partial class Hammer : GameObject
{
    private enum State { Idle, Charging, Charged, Swing, Cooldown }
    private State state = State.Idle;

    [Export] public Sprite2D sprite;
    [Export] public CircleShape2D circleShape;

    [Export] private float maxChargeTime = 1f;
    [Export] private float baseAttackTime = 0.1f;
    [Export] private float attackTimeGrowth = 0.05f;

    [Export] private float idleRotation = 120f;
    [Export] private float startChargeRotation = -20f;
    [Export] private float chargedRotation = -60f;

    [Export] private float chargeTweenTime = 0.1f;
    [Export] private float chargedTweenTime = 3f;

    private Tween tween;

    private float chargeTime;
    private float attackTime;
    private float cooldownTime;

    private static Hammer instance;

    public static Hammer GetInstance()
    {
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null)
            instance = this;
        else
            QueueFree();
    }

    public override void Initialize()
    {
        base.Initialize();
        enable = false;
        attackTime = baseAttackTime;
    }

    public void Attack(float pDelta)
    {
        switch (state)
        {
            case State.Idle:
                if (Input.IsActionJustPressed("SPECIAL"))
                {
                    chargeTime = 0f;
                    StartChargeTween();
                    state = State.Charging;
                }
                break;

            case State.Charging:
                chargeTime += pDelta;

                if (chargeTime >= maxChargeTime)
                {
                    ChargeTween();
                    state = State.Charged;
                }

                if (Input.IsActionJustReleased("SPECIAL"))
                {
                    IdleTween();
                    state = State.Idle;
                }
                break;

            case State.Charged:
                attackTime += attackTimeGrowth * pDelta;
                if (Input.IsActionJustReleased("SPECIAL"))
                    StartSwing();
                break;

            case State.Swing:
                break;

            case State.Cooldown:
                cooldownTime -= pDelta;
                if (cooldownTime <= 0f)
                {
                    attackTime = baseAttackTime;
                    IdleTween();
                    state = State.Idle;
                    enable = false;
                }
                break;
        }
    }

    private void IdleTween()
    {
        KillTween();
        tween = CreateTween();
        tween.TweenProperty(sprite, "rotation_degrees", idleRotation, maxChargeTime)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Back);
    }

    private void StartChargeTween()
    {
        KillTween();
        tween = CreateTween();
        tween.TweenProperty(sprite, "rotation_degrees", startChargeRotation, maxChargeTime - chargeTweenTime)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Back);
    }

    private void ChargeTween()
    {
        KillTween();
        tween = CreateTween();
        tween.TweenProperty(sprite, "rotation_degrees", chargedRotation, chargedTweenTime)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Back);
    }

    private void StartSwing()
    {
        state = State.Swing;
        cooldownTime = chargeTime;

        enable = true;
        Player.GetInstance().Dash(attackTime);

        KillTween();
        tween = CreateTween();
        tween.TweenProperty(sprite, "rotation_degrees", idleRotation, attackTime)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Back);

        tween.Finished += () => state = State.Cooldown;
    }

    private void TeleportSwing(Vector2 enemyPos)
    {
        KillTween();
    }

    private void KillTween()
    {
        if (tween != null && tween.IsRunning())
            tween.Kill();
    }

    protected override void OnCollide(Area2D pArea)
    {
        if (pArea is Entity lEntity && state == State.Swing)
        {
            lEntity.TakeDamage();
            TeleportSwing(lEntity.GlobalPosition);
        }

    }
}
