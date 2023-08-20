using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;


namespace MgEngine.Obj
{
    public class Animation
    {
        private List<Rectangle> _animation = new();
        private List<int> _frameTimeList;

        private int _currentFrameIndex;
        private int _currentFrameTime;


        public Animation(int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            _frameTimeList = frameTimeList;

            for (int i = 0; i < frameTimeList.Count(); i++)
            {
                _animation.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));

            }
        }

        public void Update()
        {
            if (_currentFrameTime >= _frameTimeList[_currentFrameIndex])
            {
                if (_currentFrameIndex < _frameTimeList.Count() - 1)
                {
                    _currentFrameIndex++;
                }
                else
                {
                    _currentFrameIndex = 0;
                    _currentFrameTime = 0;
                }
            }

            _currentFrameTime++;
        }

        public Rectangle GetCurrentFrame()
        {
            return _animation[_currentFrameIndex];
        }
    }
}
