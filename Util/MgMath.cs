
using System;
using Microsoft.Xna.Framework;

namespace MgEngine.Util
{
    public static class MgMath
    {
        public static float ToRadians(float angleDegrees)
        {
            return angleDegrees * MathF.PI / 180;
        }

        public static float ToDegrees(float angleRadians)
        {
            return angleRadians / MathF.PI / 180f;
        }

        public static void Normalize(ref float x, ref float y)
        {
            float invLen = 1f / MathF.Sqrt(x * x + y * y);
            x *= invLen;
            y *= invLen;
        }

        public static Vector2 RotateVectorCenter(Vector2 vector, Vector2 center, float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            float x = (vector.X - center.X) * cos - (vector.Y - center.Y) * sin + center.X;
            float y = (vector.X - center.X) * sin + (vector.Y - center.Y) * cos + center.Y;

            return new Vector2(x, y);
        }
    }
}
