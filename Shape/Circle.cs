using Microsoft.Xna.Framework;
using System;


namespace MgEngine.Shape
{
    public class Circle
    {
        private Vector2[] _vertices;

        private float _x;
        private float _y;
        private float _radius;
        private int _points;

        public Circle(int x, int y, float radius, int points = 10)
        {
            _x = x;
            _y = y;
            _radius = radius;
            _points = points;

            CalculateVertices();
        }

        #region Properties
        public Vector2[] Vertices { get { return _vertices; } }

        public int X
        {
            get { return (int)_x; }

            set
            {
                _x = value;
                CalculateVertices();
            }
        }


        public int Y
        {
            get { return (int)_y; }

            set
            {
                _y = value;
                CalculateVertices();
            }
        }

        #endregion

        private void CalculateVertices()
        {
            _vertices = new Vector2[_points];

            float deltaAngle = MathHelper.TwoPi / (float)_points;
            float angle = 0;

            for (int i = 0; i < _points; i++)
            {
                float vx = MathF.Cos(angle) * _radius + _x;
                float vy = MathF.Sin(angle) * _radius + _y;

                _vertices[i] = new Vector2(vx, vy);
                //_vertices[i] *= 0.5f;

                angle += deltaAngle;
            }
        }
    }
}
