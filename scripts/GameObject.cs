using Godot;
using System;

public partial class GameObject : Area2D
{
	public bool enable;

	protected RandomNumberGenerator rand = new RandomNumberGenerator();

    public virtual void Initialize()
    {
        AreaEntered += OnCollide;

        enable = true;

		GD.Print(Name + " is ready");
    }

    public override void _Process(double pDelta)
    {
        if (!enable) return;

        DoAction((float)pDelta);
    }


    protected virtual void DoAction(float pDelta) { }
	protected virtual void OnCollide(Area2D pArea) { }
}
