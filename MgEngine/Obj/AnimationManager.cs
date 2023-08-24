using System;
using System.Collections.Generic;
using System.Drawing;

namespace MgEngine.Obj
{
    public class AnimationManager
    {
        private Dictionary<object, Animation> _animations;
        private object _currentAnimationKey;

        public AnimationManager()
        {
            _animations = new();
        }

        public void AddAnimation(object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            _animations.Add(actionKey, new Animation(frameWidth, frameHeight, frameTimeList, row));
        }

        public void Update(float dt)
        {
                if (_animations.ContainsKey(_currentAnimationKey))
                {
                    _animations[_currentAnimationKey].Update(dt);
                }
           
        }

        public void SetAnimation(object actionKey)
        {
            if (_animations.ContainsKey(actionKey))
            {
                _animations[_currentAnimationKey].Reset();
                _currentAnimationKey = actionKey;

            }
            else
            {
                Console.WriteLine($"Key {actionKey} does not exists!");
            }
        }

        public Animation CurrentAnimation
        {
            get{ return _animations[_currentAnimationKey]; }
        }
    }
}
