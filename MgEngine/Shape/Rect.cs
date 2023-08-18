using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace mgengine.Shape
{
    public class Rect
    {
        public Vector2 pos;

        public int width;

        public int height;
        
        private Texture2D _texture;

        private Color _color;

        public Rect(int x, int y, int width, int height)
        {
            this.pos = new Vector2(x, y);
            this.width = width;
            this.height = height;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, GetRectangle(), _color);
        }

        public void SetColor(GraphicsDevice graphicsDevice, Color color)
        {
            _color = color;

            _texture = new Texture2D(graphicsDevice, width, height);

            Color[] textureData = new Color[width * height];
            for (int i = 0; i < textureData.Length; i++)
            {
                textureData[i] = color;
            }

            _texture.SetData(textureData);
        }
    }
}