using MgEngine.Interface;
using MgEngine.Shape;
using Microsoft.Xna.Framework;

namespace MgEngine.Component
{
    public class Box2D : IBox2D
    {
        public Vector2 Pos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }

        public Box2D()
        {
        }

        public Box2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Box2D(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float X
        {
            get { return (int)Pos.X; }

            set { Pos = new Vector2(value, Pos.Y); }
        }

        public float Y
        {
            get { return (int)Pos.Y; }

            set { Pos = new Vector2(Pos.X, value); }
        }

        public Rect Rect
        {
            get { return new Rect(X, Y, Width, Height); }
        }

        public void ResizeScale(float scale)
        {
            Width = (int)(Width * scale);
            Height = (int)(Height * scale);
        }

        public Vector3 Pos3
        {
            get { return new Vector3(Pos, 0); }
        }
    }


}
