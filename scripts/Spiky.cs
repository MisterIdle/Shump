using Godot;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

public partial class Spiky : ShootingEnemy
{
    public enum MoveState
    {
        ChasePlayer,
        FleePlayer,
    }

    [Export] private int numSpike = 10;
    [Export] private float distance = 50f;
    [Export] private float duration = 0.5f;
    [Export] private float animation = 0.1f;
    [Export] private int numSpikeActif = 2;

    [Export] private float minDistance = 400f;
    [Export] private float maxDistance = 500f;

    [Export] private float returnSpeed = 1.2f;

    private List<Rocket> spikesList = new();
    private bool attackCheck = true;

    private MoveState moveState = MoveState.ChasePlayer;

    protected override void DoMove(float pDelta)
    {
        base.DoMove(pDelta);

        float lPlayerDistance = DistanceTo(Player.GetInstance());
        playerDirection = DirectionTo(Player.GetInstance());

        velocity = playerDirection;

        if (lPlayerDistance >= minDistance)
            moveState = MoveState.ChasePlayer;
        else
            moveState = MoveState.FleePlayer;



        switch (moveState)
        {
            case MoveState.ChasePlayer:
                velocity = playerDirection;
                break;

            case MoveState.FleePlayer:
                velocity = -playerDirection;
                break;
        }
    }

    protected override void DoAttack()
    {
        base.DoAttack();

        if (attackCheck)
            SpawnSpikes();
        else
            ActivateRandomRockets(numSpikeActif);

        attackCheck = !attackCheck;
    }

    private void SpawnSpikes()
    {
        float lAngleStep = 2 * Mathf.Pi / numSpike;

        for (int i = 0; i < numSpike; i++)
        {
            if (i < spikesList.Count && spikesList[i] != null && !spikesList[i].isActive)
                continue;

            Rocket lRocket = Rocket.Create(shootScene, Player.GetInstance(), Vector2.Zero);

            Vector2 lDirection = Vector2.FromAngle(lAngleStep * i);
            Vector2 lTargetPosition = lDirection * distance;

            lRocket.Position = Vector2.Zero;
            lRocket.Rotation = lDirection.Angle();

            if (i < spikesList.Count)
                spikesList[i] = lRocket;
            else
                spikesList.Add(lRocket);

            AddChild(lRocket);

            Tween lTween = CreateTween();
            lTween.TweenProperty(lRocket, "position", lTargetPosition, duration)
                  .SetTrans(Tween.TransitionType.Back)
                  .SetEase(Tween.EaseType.InOut)
                  .SetDelay(i * animation);
        }
    }

    private void ActivateRandomRockets(int pCount)
    {
        List<Rocket> lInactiveRockets = new();

        foreach (Rocket lRocket in spikesList)
        {
            if (lRocket != null && !lRocket.isActive)
                lInactiveRockets.Add(lRocket);
        }

        for (int i = 0; i < pCount && lInactiveRockets.Count > 0; i++)
        {
            int lIndex = rand.RandiRange(0, lInactiveRockets.Count - 1);
            Rocket lRocket = lInactiveRockets[lIndex];
            lInactiveRockets.RemoveAt(lIndex);

            FireSpike(lRocket);
        }
    }

    private void FireSpike(Rocket pRocket)
    {
        Tween lTween = CreateTween();
        lTween.TweenProperty(pRocket, "modulate", Colors.Red, duration)
              .SetTrans(Tween.TransitionType.Quad)
              .SetEase(Tween.EaseType.InOut);

        lTween.Finished += () => LaunchSpike(pRocket);
    }

    private void LaunchSpike(Rocket pRocket)
    {
        Vector2 lGlobalPosition = pRocket.GlobalPosition;
        float lGlobalRotation = pRocket.GlobalRotation;

        RemoveChild(pRocket);
        GameManager.GetInstance().AddChild(pRocket);

        pRocket.GlobalPosition = lGlobalPosition;
        pRocket.GlobalRotation = lGlobalRotation;
        pRocket.isActive = true;
    }
}
