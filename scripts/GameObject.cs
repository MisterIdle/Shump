using Godot;
using System;

public partial class GameObject : Area2D
{
	[Export] public bool enable;

	protected GameManager gameManager;
	[Export] protected Node2D gameContainer;

	public virtual void Initialize()
	{
		gameManager = GameManager.GetInstance();
		gameContainer = gameManager.gameContainer;

        AreaEntered += OnCollide;
        enable = true;
    }

	public override void _Process(double pDelta)
	{
		if (!enable) return;

		float lDelta = (float)pDelta;
		DoAction(lDelta);
	}

	protected virtual void DoAction(float pDelta) { }
	protected virtual void OnCollide(Area2D pArea) { }
}
