using Godot;

public static class Shaker
{
    public static void Shake(Node2D target, float time, float frequency, float amplitude = 8f)
    {
        Vector2 origin = target.Position;
        int shakes = Mathf.Max(1, Mathf.RoundToInt(time * frequency));
        float step = time / shakes;

        var tween = target.GetTree().CreateTween();

        for (int i = 0; i < shakes; i++)
        {
            Vector2 offset = new Vector2(
                (float)GD.RandRange(-amplitude, amplitude),
                (float)GD.RandRange(-amplitude, amplitude)
            );

            tween.TweenProperty(target, "position", origin + offset, step);
        }

        tween.TweenProperty(target, "position", origin, step);
    }
}
