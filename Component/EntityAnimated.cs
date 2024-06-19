using MgEngine.Util;
using Microsoft.Xna.Framework.Graphics;

#pragma warning disable CS8618
namespace MgEngine.Component
{
    public class EntityAnimated : Entity
    {
        Animator _animator;
        bool _initialized;

        public EntityAnimated() : base()
        {
        }

        public EntityAnimated(Animator animator) : base()
        {
            _animator = animator;
        }

        #region Methods

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAction(object actionKey)
        {
            if (_initialized && _animator.CurrentAction == actionKey)
                return;

            _animator.SetAction(actionKey);
            _texture = _animator.GetTexture(actionKey);
            _sourceRectangle = _animator.GetCurrentFrame();

            if (!_initialized)
            {
                Width = _sourceRectangle.Width;
                Height = _sourceRectangle.Height;
                ResizeScale(MgDefault.Scale);
                _initialized = true;
            }
        }

        public void Animate(float dt)
        {
            _animator.Update(dt);
            _sourceRectangle = _animator.GetCurrentFrame();
        }

        #endregion
    }
}
