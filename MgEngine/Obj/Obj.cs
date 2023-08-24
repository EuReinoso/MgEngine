using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

using MgEngine.Shape;

namespace MgEngine.Obj
{
    public class Obj : Box2D
    {
        private Texture2D _currentTexture;

        private Rectangle _currentFrame;
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;

        public Obj()
        {

        }


        public Rect Rect 
        { 
            get { return new Rect(X, Y, Width, Height); } 
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentTexture != null)
            {
                spriteBatch.Draw(_currentTexture, Pos, _currentFrame, Color.White, Rotation, Center, 4, SpriteEffects.None, 1);
            }
            else
            {
                Console.WriteLine("Attempting to Draw an Obj without Texture");
            }
        }

        #region Animation
        public void LoadTexture(ContentManager content, string path)
        {
            try
            {
                _currentTexture = content.Load<Texture2D>(path);
                _currentFrame = new Rectangle(0, 0, _currentTexture.Width, _currentTexture.Height);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InitAnimation()
        {
            _animations = new();
            _textures = new();
        }

        public void AddAnimation(Texture2D texture, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            if (!_textures.ContainsKey(actionKey))
            {
                _textures.Add(actionKey, texture);
                _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row);
            }
            else
            {
                Console.WriteLine($"ActionKey `{actionKey}` already exists at this Obj!");
            }
        }

        public void SetAction(object actionKey)
        {
            _currentTexture = _textures[actionKey];
            _animations.SetAnimation(actionKey);
        }

        public void Animate(float dt)
        {
            if (_animations != null)
            {
                try
                {
                    _animations.Update(dt);
                    _currentFrame = _animations.CurrentAnimation.CurrentFrame;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    
                }
                
            }
            else
            {
                throw new Exception("Attempt to Animate without InitAnimation!");
            }
        }
        #endregion

    }
}
