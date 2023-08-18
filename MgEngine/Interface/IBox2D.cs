using Microsoft.Xna.Framework;

namespace mgengine.Interface
{
    public interface IBox2D
    {
        public Vector2 Pos { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
