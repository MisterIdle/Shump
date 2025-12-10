using Godot;
using System;

public partial class GameObject : Area2D
{
	[Export] public bool enable;

	protected RandomNumberGenerator rand = new RandomNumberGenerator();
	protected GameManager gameManager;
	protected Node2D gameContainer;

	public virtual void Initialize()
	{
		gameManager = GameManager.GetInstance();
		gameContainer = gameManager.gameContainer;

        AreaEntered += OnCollide;
        enable = true;

		GD.Print(Name + " is ready");
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
