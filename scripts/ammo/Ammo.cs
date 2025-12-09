using Godot;

public partial class Ammo : Movable
{
    [Export] protected GameObject shooter;

    public override void Initialize()
    {
        base.Initialize();
        gameManager.gameContainer.AddChild(this);
    }
}
