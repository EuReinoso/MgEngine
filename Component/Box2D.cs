using MgEngine.Interface;
using MgEngine.Shape;
using Microsoft.Xna.Framework;
using System;

namespace MgEngine.Component
{
    public class Box2D : RigidBody, IBox2D
    {
        private int _width { get; set; }
        private int _height { get; set; }
        public Vector2 _pos { get; set; }
        

        public event Action? OnResize;

        public event Action? OnMove;

        public Box2D() : base(ShapeType.Rect)
        {
        }

        public Box2D(float x, float y) : base(ShapeType.Rect)
        {
            X = x;
            Y = y;
        }

        public Box2D(float x, float y, int width, int height) : base(ShapeType.Rect)
        {
            X = x;
            Y = y;
            _width = width;
            _height = height;
        }

        public int Width { 
            get { return _width; }

            set
            {
                if (_width == value)
                    return;

                _width = value;
                OnResize?.Invoke();
            }
        }

        public int Height
        {
            get { return _height; }

            set
            {
                if (_height == value)
                    return;

                _height = value;
                OnResize?.Invoke();
            }
        }

        public Vector2 Pos
        {
            get { return _pos; }

            set
            {
                if (_pos == value)
                    return;

                _pos = value;
                OnMove?.Invoke();
            }
        }

        public float X
        {
            get { return _pos.X; }

            set { Pos = new Vector2(value, _pos.Y); }
        }

        public float Y
        {
            get { return _pos.Y; }

            set { Pos = new Vector2(_pos.X, value); }
        }

        public Rect Rect
        {
            get { return new Rect(X, Y, _width, _height); }
        }

        public void ResizeScale(float scale)
        {
            if (scale == 1)
                return;

            _width = (int)(_width * scale);
            _height = (int)(_height * scale);

            OnResize?.Invoke();
        }

        public Vector3 Pos3
        {
            get { return new Vector3(_pos, 0); }
        }
    }


}
