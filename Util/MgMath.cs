
using System;

namespace MgEngine.Util
{
    public static class MgMath
    {
        public static float ToRadians(float angleDegrees)
        {
            return (float)(angleDegrees * (Math.PI / 180));
        }

        public static float ToDegrees(float angleRadians)
        {
            return (float)(angleRadians / (Math.PI / 180));
        }

        public static void Normalize(ref float x, ref float y)
        {
            float invLen = 1f / MathF.Sqrt(x * x + y * y);
            x *= invLen;
            y *= invLen;
        }
    }
}
