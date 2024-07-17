using MgEngine.Util;
using Microsoft.Xna.Framework.Graphics;

#pragma warning disable CS8618
namespace MgEngine.Component
{
    public class EntityAnimated : Entity
    {
        Animator _animator;
        private object _currentAction;
        public bool PriorityActionActive { get; set; }

        public EntityAnimated() : base()
        {
        }

        public EntityAnimated(Animator animator) : base()
        {
            _animator = animator;
        }

        public object CurrentAction { get{ return _currentAction; } }

        #region Methods

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAction(object actionKey)
        {
            if (PriorityActionActive || (_firstTextureLoaded && _animator.CurrentAction == actionKey))
                return;

            _animator.SetAction(actionKey);
            SetTexture(_animator.GetTexture(actionKey), _animator.GetCurrentFrame(), _animator.GetOffSet(actionKey));
            _currentAction = actionKey;
        }

        public void Animate(float dt)
        {
            _animator.Update(dt);
            _sourceRectangle = _animator.GetCurrentFrame();
        }

        #endregion
    }
}
