using MgEngine.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Util
{
    public static class MgUtil
    {
        public static Texture2D GetTexture2D(ContentManager content, string path)
        {
            return content.Load<Texture2D>(path);
        }

        public static Color RandomColor()
        {
            return new Color(new Random().Next(0, 255), new Random().Next(0, 255), new Random().Next(0, 255));
        }

        public static Vector2 RandomWindowPos(Window window, int margin = 0)
        {
            return new Vector2(new Random().Next(0 + margin, window.Width - margin), new Random().Next(0, window.Height - margin));
        }

        public static Vector2 RandomCanvasPos(Canvas canvas, int margin = 0)
        {
            return new Vector2(new Random().Next(0 + margin, canvas.Width - margin), new Random().Next(0, canvas.Height - margin));
        }

        public static Vector2 RandomPos(int minX, int maxX, int minY, int maxY)
        {
            return new Vector2(new Random().Next(minX, maxX), new Random().Next(minY, maxY));
        }

        public static Color ColorLight(Color color, float factor)
        {
            factor = MgMath.Clamp(factor, 0f, 1f);

            int red = (int)(color.R * factor);
            int green = (int)(color.G * factor);
            int blue = (int)(color.B * factor);

            return new Color(red, green, blue, color.A);
        }

    }
}
