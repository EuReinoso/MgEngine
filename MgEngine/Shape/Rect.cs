using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Obj;

namespace MgEngine.Shape
{
    public class Rect : Box2D
    {    
        private Texture2D _texture;

        private Color _color;

        public Rect(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, GetRectangle(), _color);
        }

        public void SetColor(GraphicsDevice graphicsDevice, Color color)
        {
            _color = color;

            _texture = new Texture2D(graphicsDevice, Width, Height);

            Color[] textureData = new Color[Width * Height];
            for (int i = 0; i < textureData.Length; i++)
            {
                textureData[i] = color;
            }

            _texture.SetData(textureData);
        }
    }
}