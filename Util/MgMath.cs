
using System;
using Microsoft.Xna.Framework;

namespace MgEngine.Util
{
    public static class MgMath
    {
        private static Random _random = new Random();

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

        public static float Clamp(float val, float min, float max) 
        {
            if (val < min)
                return min;

            if (val > max)
                return max;

            return val;
        }

        public static int Clamp(int val, int min, int max)
        {
            if (val < min)
                return min;

            if (val > max)
                return max;

            return val;
        }

        public static int RandInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static float RandFloat(float min, float max)
        {
            return _random.NextSingle() * (max - min) + min;
        }

        public static float GetRotationDirection(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }
    }
}
