using MgEngine.Component;
using MgEngine.Util;
using Microsoft.Xna.Framework;

namespace MgEngine.Shape
{
    public class Rect : Box2D
    {

        private Vector2[] _vertices;

        public Rect(float x, float y, int width, int height, float rotation = 0) : base(x, y, width, height)
        {
            _vertices = new Vector2[4];
            Rotation = rotation;
            CalculateVertices();
        }

        public Rect(Rectangle rectangle) : base(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
            _vertices = new Vector2[4];
            CalculateVertices();
        }

        #region Properties

        public float Left { get { return X - Width / 2; } }
        public float Right { get { return X + Width / 2; } }
        public float Top { get { return Y - Height / 2; } }
        public float Bottom { get { return Y + Height / 2; } }

        public Vector2[] Vertices { get { return _vertices; } }

        public Rectangle Rectangle { get { return new Rectangle((int)X, (int)Y, Width, Height); } }

        public new float X
        {
            get { return Pos.X; }

            set
            {
                Pos = new Vector2(value, Pos.Y);
                CalculateVertices();
            }
        }

        public new float Y
        {
            get { return Pos.Y; }

            set
            {
                Pos = new Vector2(Pos.X, value);
                CalculateVertices();
            }
        }

        public new int Width
        {
            get { return base.Width; }

            set
            {
                base.Width = value;
                CalculateVertices();
            }
        }

        public new int Height
        {
            get { return base.Height; }

            set
            {
                base.Height = value;
                CalculateVertices();
            }
        }

        public new float Rotation
        {
            get { return base.Rotation; }

            set
            {
                base.Rotation = value;
                CalculateVertices();
            }
        }

        public new Vector2 Pos 
        { 
            get { return base.Pos; }

            set
            {
                base.Pos = value;

                CalculateVertices();
            }
        }

        #endregion
        private void CalculateVertices()
        {
            _vertices[0] = new Vector2(Left, Top);
            _vertices[1] = new Vector2(Right, Top);
            _vertices[2] = new Vector2(Right, Bottom);
            _vertices[3] = new Vector2(Left, Bottom);

            if (Rotation == 0)
                return;

            for(int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = MgMath.RotateVectorCenter(_vertices[i], Pos, Rotation);
            }
        }



    }
}