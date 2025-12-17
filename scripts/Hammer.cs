using Godot;
using System.Collections.Generic;

public partial class Hammer : GameObject
{
    private enum State { Idle, Charging, Charged, Swing, Cooldown }
    private State state;

    private List<Entity> hitEntity = new List<Entity>();

    [Export] private Sprite2D sprite;
    [Export] private TextureProgressBar progressBar;
    [Export] private GpuParticles2D particule;

    [Export] private float maxChargeTime = 1f;
    [Export] private float maxChargedTime = 3f;
    [Export] private float baseAttackTime = 0.1f;
    [Export] private float attackTimeGrowth = 0.15f;
    [Export] private float attackTimeDouble = 1.5f;

    [Export] private float idleRotation = 120f;
    [Export] private float startChargeRotation = -20f;
    [Export] private float chargedRotation = -60f;

    [Export] private float swingSingleRotation = 120f;
    [Export] private float swingDoubleRotation = 480f;

    [Export] private float chargeTweenTime = 0.1f;
    [Export] private float chargedTweenTime = 3f;

    [Export] private float hammerHide = 0.2f;

    private Tween tween;
    private Timer animationTimer;

    private float chargeTime;
    private float attackTime;

    private static Hammer instance;
    public static Hammer GetInstance() 
    {  
        return instance;
    }

    public override void _Ready()
    {
        if (instance == null) instance = this;
        else QueueFree();
    }

    protected override void DoAction(float pDelta)
    {
        base.DoAction(pDelta);
        particule.GlobalPosition = GlobalPosition;
    }

    public override void Initialize()
    {
        base.Initialize();
        enable = false;

        state = State.Idle;
        attackTime = baseAttackTime;

        animationTimer = new Timer();
        AddChild(animationTimer);

        Player.GetInstance().invulnerability = false;
        SetRotation(idleRotation, maxChargeTime);
    }

    public void Attack(float pDelta)
    {
        progressBar.Value = chargeTime;

        if (chargeTime <= 0) chargeTime = 0;

        if (state == State.Swing)
            SwingAttack();

        switch (state)
        {
            case State.Idle:
                if (Input.IsActionJustPressed("SPECIAL"))
                    StartCharging();
                break;

            case State.Charging:
                chargeTime += pDelta;
                progressBar.TintProgress = Colors.Yellow;

                if (chargeTime >= maxChargeTime)
                    FullyCharged();

                if (Input.IsActionJustReleased("SPECIAL"))
                    ResetToIdle();

                break;

            case State.Charged:
                chargeTime += pDelta;
                attackTime += attackTimeGrowth * pDelta;

                progressBar.TintProgress = Colors.Orange;

                if (Input.IsActionJustReleased("SPECIAL") || chargeTime > maxChargedTime)
                    StartSwing();
                break;

            case State.Cooldown:
                chargeTime -= pDelta;

                if (chargeTime <= 0f)
                    EndCooldown();
                break;
        }
    }

    private void StartCharging()
    {
        particule.Emitting = true;
        progressBar.Visible = true;
        sprite.Visible = true;

        state = State.Charging;
        SetRotation(startChargeRotation, maxChargeTime - chargeTweenTime);
    }

    private void FullyCharged()
    {
        progressBar.TintProgress = Colors.Orange;
        state = State.Charged;
        SetRotation(chargedRotation, chargedTweenTime);
    }

    private void StartSwing()
    {
        state = State.Swing;

        progressBar.TintProgress = Colors.Red;

        Player.GetInstance().invulnerability = true;
        Player.GetInstance().Dash(attackTime / 2);

        if (tween != null && tween.IsRunning()) tween.Kill();
        tween = CreateTween();

        if (attackTime !< attackTimeDouble)
        {
            tween.TweenProperty(sprite, "rotation_degrees", swingSingleRotation, attackTime)
                 .SetEase(Tween.EaseType.InOut)
                 .SetTrans(Tween.TransitionType.Back);
        } 
        else
        {
            tween.TweenProperty(sprite, "rotation_degrees", swingDoubleRotation, attackTime)
                .SetEase(Tween.EaseType.InOut)
                .SetTrans(Tween.TransitionType.Back);
        }

            tween.Finished += FinishAttack;
    }


    private void EndCooldown()
    {
        attackTime = baseAttackTime;
        ResetToIdle();
    }

    private void ResetToIdle()
    {
        chargeTime = 0;
        progressBar.TintProgress = Colors.Yellow;

        progressBar.Visible = false;
        particule.Emitting = true;
        sprite.Visible = false;

        state = State.Idle;
        SetRotation(idleRotation, 0);
    }

    private void SwingAttack()
    {
        float lRange = 75f;

        foreach (Entity lGlobalEntity in GetOverlappingAreas())
            if (lGlobalEntity is Entity lEntity)
            {
                float lDistance = sprite.GlobalPosition.DistanceTo(lEntity.GlobalPosition);
                if (lDistance <= lRange)
                {
                    lEntity.TakeDamage();
                }
                else
                    hitEntity.Add(lEntity);
            }
    }

    private void FinishAttack()
    {
        for (int i = hitEntity.Count - 1; i >= 0; i--)
        {
            if (IsInstanceValid(hitEntity[i]))
                hitEntity[i].TakeDamage();
            hitEntity.RemoveAt(i);
        }

        state = State.Cooldown;

        animationTimer.WaitTime = hammerHide;
        animationTimer.OneShot = true;

        animationTimer.Timeout += FinishAttackAnimation;
        animationTimer.Start();

        Player.GetInstance().invulnerability = false;
    }
    private void FinishAttackAnimation()
    {
        sprite.Visible = false;
        particule.Emitting = true;
    }

    private void SetRotation(float pTarget, float pTime)
    {
        if (tween != null && tween.IsRunning()) tween.Kill();

        tween = CreateTween();
        tween.TweenProperty(sprite, "rotation_degrees", pTarget, pTime)
             .SetEase(Tween.EaseType.InOut)
             .SetTrans(Tween.TransitionType.Back);
    }
}
