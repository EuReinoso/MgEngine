using MgEngine.Util;
using Microsoft.Xna.Framework;

namespace MgEngine.Shape
{
    public class Polygon
    {
        public List<Vector2> Vertices;
        public Vector2 Center;
        private float _rotation;

        public Polygon(List<Vector2> vertices)
        {
            Vertices = vertices;
            CalculateCenter();
        }

        public Polygon(Vector2[] vertices)
        {
            Vertices = vertices.ToList();
            CalculateCenter();
        }

        public float Rotation
        {
            get { return _rotation;}

            set
            {
                _rotation = value;

                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i] = MgMath.RotateVectorCenter(Vertices[i], Center, _rotation);
                }
            }
        }

        public void CalculateCenter()
        {
            if (Vertices.Count == 0)
                Center = Vector2.Zero;

            Vector2 sum = new Vector2();

            foreach (Vector2 v in Vertices)
            {
                sum += v;
            }

            Center = sum / Vertices.Count;
        }

    }
}
