﻿using Microsoft.Xna.Framework;
using System;

namespace MgEngine.Shape
{
    public class Circle
    {
        #region Variables
        private Vector2[] _vertices;

        private float _x;
        private float _y;
        private float _radius;
        private int _points;

        public bool Filled;
        public int LineWidth;
        #endregion

        #region Constructor
        public Circle(int x, int y, float radius, int points = 10, bool filled = true, int lineWidth = 1)
        {
            _x = x;
            _y = y;
            _radius = radius;
            _points = points;
            _vertices = new Vector2[_points];

            Filled = filled;
            LineWidth = lineWidth;

            CalculateVertices(false);
        }
        #endregion

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

        #endregion

        #region Methods
        private void CalculateVertices(bool resizeVertices = true)
        {
            if (resizeVertices)
                _vertices = new Vector2[_points];

            float deltaAngle = MathHelper.TwoPi / (float)_points;
            float angle = 0;

            for (int i = 0; i < _points; i++)
            {
                float vx = MathF.Cos(angle) * _radius + _x;
                float vy = MathF.Sin(angle) * _radius + _y;

                _vertices[i] = new Vector2(vx, vy);

                angle += deltaAngle;
            }
        }
        #endregion
    }
}