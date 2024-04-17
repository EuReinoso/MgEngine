using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Component
{
    public class EntityAnimated : Entity
    {
        Animator _animator;

        public EntityAnimated(Animator animator) : base()
        {
            _animator = animator;
        }

        #region Methods

        public void SetAction(object actionKey)
        {
            _animator.SetAction(actionKey);
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
