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
        private Texture2D _currentTexture;
        private Rectangle _currentFrame;
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;
        private Surface _surface;
        private SpritesManager _sprites;

        public Entity(SpritesManager sprites, bool animated = false)
        {
            _sprites = sprites;
            _surface = new Surface(sprites.GraphicsDevice);

            if (animated)
            {
                InitAnimation();
            }
        }

        public Rect Rect
        {
            get { return new Rect(X, Y, Width, Height); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentTexture == null)
            {
                throw new Exception("Attempting to Draw an Obj without Texture");
            }

            RenderTarget2D renderTarget = _surface.DrawTextureOnSurface(_currentTexture, new Rectangle(0, 0, Width, Height), _currentFrame);

            _sprites.SetMainRenderTarget();

            spriteBatch.Draw(renderTarget, Pos, new Rectangle(0, 0, Width, Height), Color.White, Rotation, Center, 1, SpriteEffects.None, 1);

            //ToDo: Fazer esse codigo funcionar ao usar resizing
            //spriteBatch.Draw(_currentTexture, Rect.Rectangle, _currentFrame, Color.White, Rotation, Center, SpriteEffects.None, 1);
        }

        public void DrawRect(Color color)
        {
            _sprites.DrawRect(Rect, color);
        }

        #region Animation
        public void LoadTexture(ContentManager content, string path)
        {
            _currentTexture = content.Load<Texture2D>(path);
            _currentFrame = new Rectangle(0, 0, _currentTexture.Width, _currentTexture.Height);
            Width = _currentFrame.Width;
            Height = _currentFrame.Height;
        }
        public void InitAnimation()
        {
            _animations = new();
            _textures = new();
        }

        public void AddAnimation(Texture2D texture, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            _textures.Add(actionKey, texture);
            _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row);
        }

        public void SetAction(object actionKey)
        {
            _currentTexture = _textures[actionKey];
            _animations.SetAnimation(actionKey);
            _currentFrame = _animations.CurrentAnimation.CurrentFrame;
            Width = _currentFrame.Width;
            Height = _currentFrame.Height;
        }

        public void Animate(float dt)
        {
            if (_animations == null)
                throw new Exception("Attempt to Animate without InitAnimation!");

            _animations.Update(dt);
            _currentFrame = _animations.CurrentAnimation.CurrentFrame;
        }
        #endregion

    }
}
