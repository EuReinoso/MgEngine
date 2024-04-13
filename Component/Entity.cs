using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;
using MgEngine.Shape;
using MgEngine.Util;

namespace MgEngine.Component
{
    public class Entity : Box2D
    {
        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;

        #region Constructor
        public Entity(Texture2D texture)
        {
            _texture = texture;
            _sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            Width = texture.Width;
            Height = texture.Height;
        }

        protected Entity()
        {

        }
        #endregion

        public Vector2 SourceCenter { get { return new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2); } }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture == null)
                throw new Exception("Texture was null, you have to Add a Texture to start Animation");

            spriteBatch.Draw(_texture, Rect.Rectangle, _sourceRectangle, Color.White, Rotation, SourceCenter, SpriteEffects.None, 1);
        }

        public void DrawRect(ShapeBatch shapeBatch, Color color)
        {
            shapeBatch.DrawRect(new Rect(X - (Width / 2), Y - (Height /2), Width, Height), color);
        }

        public void ResizeScale(float scale)
        {
            Width = (int)(Width * scale);
            Height = (int)(Height * scale);
        }

    }
}
