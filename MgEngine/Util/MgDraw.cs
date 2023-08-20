using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Shape;

namespace MgEngine.Util
{
    public class MgDraw
    {
        private GraphicsDevice _graphicsDevice;

        public MgDraw(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void DrawRect(SpriteBatch spriteBatch, Rect rect, Color color, float rotation = 0)
        {
            rect.Rotation = rotation;

            rect.Load(_graphicsDevice, color);

            rect.Draw(spriteBatch);
        }

        public void DrawLine(SpriteBatch spriteBatch, Line line, Color color)
        {
            line.Load(_graphicsDevice, color);

            line.Draw(spriteBatch);
        }
    }
}
