using MgEngine.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Entity
{
    public class EntityAnimated : Entity
    {
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;

        public EntityAnimated(SpritesDraw sprites, Texture2D texture) : base(sprites, texture)
        {
            _animations = new();
            _textures = new();
            _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        #region Methods

        public void AddAnimation(Texture2D texture, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            _textures.Add(actionKey, texture);
            _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row);
        }

        public void SetAction(object actionKey)
        {
            _texture = _textures[actionKey];
            _animations.SetAnimation(actionKey);
            _sourceRectangle = _animations.CurrentAnimation.CurrentFrame;
            Width = _sourceRectangle.Width;
            Height = _sourceRectangle.Height;
        }

        public void Animate(float dt)
        {
            _animations.Update(dt);
            _sourceRectangle = _animations.CurrentAnimation.CurrentFrame;
        }

        #endregion
    }
}
