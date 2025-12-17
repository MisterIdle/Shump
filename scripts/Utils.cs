using Godot;

public static class Utils
{
    private static Timer timer;

    public static bool OutOfBound(Vector2 pPos, float pMarginX = 0, float pMarginY = 0)
    {
        Vector2 lScreen = GameManager.GetInstance().screenSize;
        Vector2 lTriggerPos = Trigger.GetInstance().Position;

        if (pPos.X < lTriggerPos.X - pMarginX || pPos.X > lScreen.X + pMarginX)
            return true;

        if (pPos.Y < lTriggerPos.Y - pMarginY || pPos.Y > lScreen.Y + pMarginY)
            return true;

        return false;
    }
}
