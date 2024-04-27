using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MgEngine.Shape;
using System;

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

        public void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            if (_texture == null)
                throw new Exception("Texture was null, you have to Add a Texture to start Animation");

            var destRectangle = new Rectangle((int)(X + scrollX), (int)(Y + scrollY), Width, Height);

            spriteBatch.Draw(_texture, destRectangle, _sourceRectangle, Color.White, Rotation, SourceCenter, SpriteEffects.None, 0f);
            //spriteBatch.Draw(_texture, Rect.Rectangle, _sourceRectangle, Color.White, Rotation, SourceCenter, SpriteEffects.None, 0f);
        }

        public void DrawRect(ShapeBatch shapeBatch, Color color)
        {
            shapeBatch.DrawRect(new Rect(X - (Width / 2), Y - (Height /2), Width, Height), color);
        }

    }
}
