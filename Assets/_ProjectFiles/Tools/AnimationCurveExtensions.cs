using UnityEngine;

public static class AnimationCurveExtensions
{
    public static float FunctionSquare(this AnimationCurve curve, int precision)
    {
        var a = curve.keys[0].time;
        var b = curve.keys[^1].time;

        var h = (b - a) / precision;
        var sum = 0.5f * (curve.Evaluate(a) + curve.Evaluate(b));

        for (var i = 1; i < precision; i++)
        {
            var x = a + i * h;
            sum += curve.Evaluate(x);
        }

        return sum * h;
    }
}