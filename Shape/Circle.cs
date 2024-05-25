using MgEngine.Component;
using Microsoft.Xna.Framework;
using System;

namespace MgEngine.Shape
{
    public class Circle : RigidBody
    {
        #region Variables
        private Vector2[] _vertices;

        private Vector2 _pos;
        private float _radius;
        private int _points;

        #endregion

        #region Constructor
        public Circle(float x, float y, float radius, int points = 10) : base(ShapeType.Circle)
        {
            _pos =  new Vector2(x, y);
            _radius = radius;
            _points = points;
            _vertices = new Vector2[_points];

            CalculateVertices(true);
        }
        #endregion

        #region Properties
        public Vector2[] Vertices { get { return _vertices; } }

        public float X
        {
            get { return _pos.X; }

            set
            {
                _pos = new Vector2(value, _pos.Y);
                CalculateVertices();
            }
        }

        public float Y
        {
            get { return _pos.Y; }

            set
            {
                _pos = new Vector2(_pos.X, value);
                CalculateVertices();
            }
        }

        public int Radius
        {
            get { return (int)_radius; }

            set
            {
                _radius = value;
                CalculateVertices();
            }
        }

        public int Points
        {
            get { return _points; }

            set
            {
                _points = value;
                CalculateVertices(false);
            }
        }

        public Vector2 Pos
        { 
            get { return _pos; }

            set 
            {
                _pos = value;
                CalculateVertices();
            }
        }

        #endregion

        #region Methods
        private void CalculateVertices(bool resizeVertices = false)
        {
            if (resizeVertices)
                _vertices = new Vector2[_points];

            float deltaAngle = MathHelper.TwoPi / (float)_points;
            float angle = -1.57079637f;

            for (int i = 0; i < _points; i++)
            {
                float vx = MathF.Cos(angle) * _radius + _pos.X;
                float vy = MathF.Sin(angle) * _radius + _pos.Y;

                _vertices[i] = new Vector2(vx, vy);

                angle += deltaAngle;
            }
        }

        public bool CollideCircle(Circle circle)
        {
            float distance = Vector2.Distance(_pos, circle.Pos);
            float radiusSum = _radius + circle.Radius;

            if (radiusSum >= distance)
                return false;

            return true;
        }

        public bool CollideCircle(Circle circle, out Vector2 normal, out float depth)
        {
            float distance = Vector2.Distance(_pos, circle.Pos);
            float radiusSum = _radius + circle.Radius;

            if (distance >= radiusSum)
            {
                normal = Vector2.Zero;
                depth = 0f;

                return false;
            }

            normal = Vector2.Normalize(circle.Pos - _pos);
            depth = radiusSum - distance;

            return true;
        }

        public static bool CollideCircle(Circle c1,  Circle c2, out Vector2 normal, out float depth)
        {
            return c1.CollideCircle(c2, out normal, out depth);
        }

        #endregion
    }
}
