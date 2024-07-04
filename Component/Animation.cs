using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MgEngine.Component
{
    public class Animation
    {
        private List<Rectangle> _animation = new();
        private List<int> _frameTimeList;

        private int _currentFrameIndex;
        private int _currentFrameTime;

        private event Action? OnReset;

        public Animation(int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1, Action? onReset = null)
        {
            _frameTimeList = frameTimeList;
            OnReset = onReset;

            for (int i = 0; i < frameTimeList.Count(); i++)
                _animation.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
        }

        public void Update(float dt)
        {
            if (_currentFrameTime >= _frameTimeList[_currentFrameIndex])
            {
                _currentFrameIndex += (int)Math.Round(1 * dt);
                _currentFrameTime = 0;

                if (_currentFrameIndex > _frameTimeList.Count() - 1)
                    Reset(true);
            }

            _currentFrameTime++;
        }

        public void Reset(bool OnResetInvoke)
        {
            _currentFrameIndex = 0;
            _currentFrameTime = 0;

            if (OnResetInvoke)
                OnReset?.Invoke();
        }

        public Rectangle CurrentFrame
        {
            get { return _animation[_currentFrameIndex]; }
        }
    }
}
