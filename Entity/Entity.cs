using MgEngine.Shape;
using MgEngine.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MgEngine.Entity
{
    public class Entity : Box2D
    {
        private SpritesDraw _sprites;
        private Surface _surface;

        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;

        public Entity(SpritesDraw sprites, Texture2D texture)
        {
            _sprites = sprites;
            _surface = new Surface(sprites.GraphicsDevice);
            _texture = texture;
            _sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            Width = texture.Width;
            Height = texture.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RenderTarget2D renderTarget = _surface.DrawTextureOnSurface(_texture, new Rectangle(0, 0, Width, Height), _sourceRectangle);

            _sprites.SetMainRenderTarget();

            spriteBatch.Draw(renderTarget, Pos, new Rectangle(0, 0, Width, Height), Color.White, Rotation, Center, 1, SpriteEffects.None, 1);

            //ToDo: Fazer esse codigo funcionar ao usar resizing
            //spriteBatch.Draw(_currentTexture, Rect.Rectangle, _currentFrame, Color.White, Rotation, Center, SpriteEffects.None, 1);
        }

        public void DrawRect(Color color)
        {
            _sprites.DrawRect(Rect, color);
        }

    }
}
