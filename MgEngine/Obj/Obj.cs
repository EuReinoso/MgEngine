using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace MgEngine.Obj
{
    public class Obj : Box2D
    {
        private Texture2D _texture;
        private Rectangle _currentFrame;
        public Animation _animation;

        public Obj()
        {
        }

        public void LoadTexture(ContentManager content, string path)
        {
            try
            {
                _texture = content.Load<Texture2D>(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Pos, _currentFrame, Color.White, Rotation, Center, Vector2.One, SpriteEffects.None, 1);
            }
            else
            {
                throw new Exception("Obj Texture not Loaded Yet");
            }
        }

        public void Animate()
        {
            _animation.Update();
            _currentFrame = _animation.GetCurrentFrame();
        }
    }
}
