using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace mgengine.Shape
{
    public class Line
    {
        public Vector2 p1;

        public Vector2 p2;

        public int width;

        private Texture2D _texture;

        private Color _color;


        public Line(Vector2 p1, Vector2 p2, int width = 1)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.width = width;
        }

        public Line(int p1x, int p1y, int p2x, int p2y, int width = 1)
        {
            this.p1 = new Vector2(p1x, p1y);
            this.p2 = new Vector2(p2x, p2y);
            this.width = width;
        }

        public void SetColor(GraphicsDevice graphicsDevice, Color color)
        {
            _color = color;

            int distance = (int)Vector2.Distance(p1, p2);

            _texture = new Texture2D(graphicsDevice, width, distance);

            Color[] textureData = new Color[width * distance];
            for (int i = 0; i < textureData.Length; i++)
            {
                textureData[i] = color;
            }

            _texture.SetData(textureData);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rotation = -(float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);

            var origin = new Vector2(0, width / 2);

            spriteBatch.Draw(_texture, p1, null, _color, rotation, origin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}