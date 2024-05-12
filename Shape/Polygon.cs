using MgEngine.Util;
using Microsoft.Xna.Framework;

namespace MgEngine.Shape
{
    public class Polygon
    {
        #region Variables
        public List<Vector2> Vertices;
        public Vector2 Center;
        private float _rotation;
        #endregion

        #region Constructor
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
        #endregion

        #region Properties
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

        #endregion

        #region Methods
        public static Vector2 GetPolygonCenter(List<Vector2> vertices)
        {
            if (vertices.Count == 0)
                return Vector2.Zero;

            Vector2 sum = new Vector2();

            foreach (Vector2 v in vertices)
            {
                sum += v;
            }

            return sum / vertices.Count;
        }

        public void CalculateCenter()
        {
            Center = GetPolygonCenter(Vertices);
        }

        #region Separating Axis Theorem
        public static bool CollidePolygon(List<Vector2> verticesA, List<Vector2> verticesB, Vector2 centerA, Vector2 centerB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = float.MaxValue;

            for (int i = 0; i < verticesA.Count; i++)
            {
                Vector2 va = verticesA[i];
                Vector2 vb = verticesA[(i + 1) % verticesA.Count];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            for (int i = 0; i < verticesB.Count; i++)
            {
                Vector2 va = verticesB[i];
                Vector2 vb = verticesB[(i + 1) % verticesB.Count];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            Vector2 direction = centerB - centerA;

            if (Vector2.Dot(direction, normal) < 0f)
            {
                normal = -normal;
            }

            return true;
        }

        public static bool CollidePolygons(List<Vector2> verticesA, List<Vector2> verticesB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = float.MaxValue;

            for (int i = 0; i < verticesA.Count; i++)
            {
                Vector2 va = verticesA[i];
                Vector2 vb = verticesA[(i + 1) % verticesA.Count];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            for (int i = 0; i < verticesB.Count; i++)
            {
                Vector2 va = verticesB[i];
                Vector2 vb = verticesB[(i + 1) % verticesB.Count];

                Vector2 edge = vb - va;
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            Vector2 centerA = GetPolygonCenter(verticesA);
            Vector2 centerB = GetPolygonCenter(verticesB);

            Vector2 direction = centerB - centerA;

            if (Vector2.Dot(direction, normal) < 0f)
            {
                normal = -normal;
            }

            return true;
        }

        public bool CollidePolygon(Polygon polygon, out Vector2 normal, out float depth)
        {
            return CollidePolygon(Vertices, polygon.Vertices, Center, polygon.Center, out normal, out depth);
        }

        #endregion

        private static void ProjectVertices(List<Vector2> vertices, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertices.Count; i++)
            {
                Vector2 v = vertices[i];
                float proj = Vector2.Dot(v, axis);

                if (proj < min) { min = proj; }
                if (proj > max) { max = proj; }
            }
        }

        public void Move(Vector2 amount)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i] += amount;
            }
        }

        #endregion
    }
}
