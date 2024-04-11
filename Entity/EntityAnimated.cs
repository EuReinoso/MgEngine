using MgEngine.Sprites;
using Microsoft.Xna.Framework.Graphics;


namespace MgEngine.Entity
{
    public class EntityAnimated : Entity
    {
        Animator _animator;

        public EntityAnimated(SpritesDraw sprites, Animator animator) : base(sprites, new Texture2D(sprites.GraphicsDevice, 1, 1))
        {
            _animator = animator;
        }

        #region Methods

        public void SetAction(object actionKey)
        {
            _texture = _animator.GetTexture(actionKey);
            _sourceRectangle = _animator.GetCurrentFrame();
            Width = _sourceRectangle.Width;
            Height = _sourceRectangle.Height;
        }

        public void Animate(float dt)
        {
            _animator.Update(dt);
            _sourceRectangle = _animator.GetCurrentFrame();
        }

        #endregion
    }
}
