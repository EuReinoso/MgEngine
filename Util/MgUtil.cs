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
            try
            {
                return content.Load<Texture2D>(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Color RandomColor()
        {
            return new Color(new Random().Next(0, 255), new Random().Next(0, 255), new Random().Next(0, 255));
        }

        public static Vector2 RandomWindowPos(Window window)
        {
            return new Vector2(new Random().Next(0, window.Width), new Random().Next(0, window.Height));
        }
    }
}
