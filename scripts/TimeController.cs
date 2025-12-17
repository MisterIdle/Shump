using Godot;

public static class TimeController
{
    public static float TimeScale = 1f;

    public static void SlowFreeze(SceneTree tree, float duration, float slowFactor = 0.05f)
    {
        TimeScale = Mathf.Clamp(slowFactor, 0.01f, 1f);
    
        SceneTreeTimer timer = tree.CreateTimer(duration, processAlways: true);
        timer.Timeout += () =>
        {
            TimeScale = 1f;
        };
    }

    public static float ScaledDelta(float pDelta)
    {
        return pDelta * TimeScale;
    }
}
