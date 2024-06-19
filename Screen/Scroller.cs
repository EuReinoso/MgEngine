using Microsoft.Xna.Framework;
using MgEngine.Component;
using MgEngine.Util;

namespace MgEngine.Screen
{

    public class Scroller
    {
        private Canvas _canvas;

        private float _trueX;
        private float _trueY;
        private float _x;
        private float _y;

        private float _delayX;
        private float _delayY;

        private int _baseWidth;
        private int _baseHeight;

        private float _zoom;

        private Vector2 _target;
        private Entity? _entityTarget;

        public float MinX { get; set;}
        public float MaxX { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }

        public Scroller(Canvas canvas)
        {
            _canvas = canvas;
            _baseWidth = canvas.Width;
            _baseHeight = canvas.Height;

            _target = canvas.Center;
            _entityTarget = null;

            _delayX = 30;
            _delayY = 5;

            _x = 0; 
            _y = 0;
            _zoom = 1;

            MinX = -10000;
            MaxX = 10000;
            MinY = -5000;
            MaxY = 5000;
        }

        public float X
        {
            get { return _x; }

            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }

            set { _y = value; }
        }

        public float Zoom
        {
            get { return _zoom; }

            set 
            {
                if ((int)(_baseWidth * value) <= 0 || (int)(_baseHeight * value) <= 0)
                    return;

                _zoom = value;
                _canvas.Width = (int)(_baseWidth * _zoom);
                _canvas.Height = (int)(_baseHeight * _zoom);
            }
        }

        public void Update()
        {
            if (_entityTarget != null)
                _target = _entityTarget.Pos;

            _trueX += (_target.X - _trueX - _canvas.Width / 2) / _delayX;
            _trueY += (_target.Y - _trueY - _canvas.Height / 2) / _delayY;

            _x = (int)_trueX;
            _y = (int)_trueY;

            _x = MgMath.Clamp(_x, MinX, MaxX);
            _y = MgMath.Clamp(_y, MinY, MaxY);
        }

        public void SetTarget(Vector2 target)
        {
            _target = target;
        }

        public void SetEntityTarget(Entity target)
        {
            _entityTarget = target;
        }

        public void FlushEntityTarget()
        {
            _entityTarget = null;
        }

        public void SetDelay(float delayX, float delayY)
        {
            if (delayX >= 1)
                _delayX = delayX;

            if (delayY >= 1)
                _delayY = delayY;
        }

        public void ResetZoom()
        {
            Zoom = 1;
        }

        public void FlushBaseSize()
        {
            _baseWidth = _canvas.Width;
            _baseHeight = _canvas.Height;
        }

        public void CalculateBorders(List<Entity> tiles, float marginX = 0, float marginY = 0)
        {
            float minX = 0;
            float maxX = 0;
            float minY = 0;
            float maxY = 0;

            foreach(var tile in tiles)
            {
                if (tile.X < minX)
                    minX = tile.X;

                if (tile.X > maxX)
                    maxX = tile.X;

                if (tile.Y < minY)
                    minY = tile.Y;

                if (tile.Y > maxY)
                    maxY = tile.Y;
            }

            MinX = minX - marginX;
            MaxX = maxX + marginX - _baseWidth;
            MinY = minY - marginY;
            MaxY = maxY + marginY - _baseHeight;
        }
    }
}
