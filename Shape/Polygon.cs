using MgEngine.Component;
using MgEngine.Util;
using Microsoft.Xna.Framework;

namespace MgEngine.Shape
{
    public class Polygon : RigidBody
    {
        #region Variables
        public List<Vector2> Vertices;
        public Vector2 Center;
        private float _rotation;
        #endregion

        #region Constructor
        public Polygon(List<Vector2> vertices, bool calculateCenter = true) : base(ShapeType.Polygon)
        {
            Vertices = vertices;

            if (calculateCenter)
                CalculateCenter();
        }

        public Polygon(Vector2[] vertices, bool calculateCenter = true) : base(ShapeType.Polygon)
        {
            Vertices = vertices.ToList();

            if (calculateCenter)
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

        public Vector2 Pos
        {
            get { return Center; }

            set
            {
                Move(value);
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

        public static bool CollidePolygon(List<Vector2> verticesA, List<Vector2> verticesB, out Vector2 normal, out float depth)
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

        public static bool CollidePolygon(Polygon polygon1, Polygon polygon2, out Vector2 normal, out float depth)
        {
            return polygon1.CollidePolygon(polygon2, out normal, out depth);
        }

        public bool CollidePolygon(Polygon polygon, out Vector2 normal, out float depth)
        {
            return CollidePolygon(Vertices, polygon.Vertices, Center, polygon.Center, out normal, out depth);
        }

        public static bool CollideCircleToPolygon(Vector2 circleCenter, float circleRadius, Vector2 polygonCenter, List<Vector2> vertices, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = float.MaxValue;

            Vector2 axis = Vector2.Zero;
            float axisDepth = 0f;
            float minA, maxA, minB, maxB;

            for (int i = 0; i < vertices.Count; i++)
            {
                Vector2 va = vertices[i];
                Vector2 vb = vertices[(i + 1) % vertices.Count];

                Vector2 edge = vb - va;
                axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(vertices, axis, out minA, out maxA);
                ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            int cpIndex = FindClosestPointOnPolygon(circleCenter, vertices);
            Vector2 cp = vertices[cpIndex];

            axis = cp - circleCenter;
            axis = Vector2.Normalize(axis);

            ProjectVertices(vertices, axis, out minA, out maxA);
            ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }

            Vector2 direction = polygonCenter - circleCenter;

            if (Vector2.Dot(direction, normal) < 0f)
            {
                normal = -normal;
            }

            return true;
        }

        public static bool CollideCircleToPolygon(Circle circle, Polygon polygon, out Vector2 normal, out float depth)
        {
            return CollideCircleToPolygon(circle.Pos, circle.Radius, polygon.Center, polygon.Vertices, out normal, out depth);
        }

        public bool CollideCircleToPolygon(Circle circle, out Vector2 normal, out float depth)
        {
            return CollideCircleToPolygon(circle.Pos, circle.Radius, Center, Vertices, out normal, out depth);
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

        private static void ProjectCircle(Vector2 center, float radius, Vector2 axis, out float min, out float max)
        {
            Vector2 direction = Vector2.Normalize(axis);
            Vector2 directionAndRadius = direction * radius;

            Vector2 p1 = center + directionAndRadius;
            Vector2 p2 = center - directionAndRadius;

            min = Vector2.Dot(p1, axis);
            max = Vector2.Dot(p2, axis);

            if (min > max)
            {
                float t = min;
                min = max;
                max = t;
            }
        }

        private static int FindClosestPointOnPolygon(Vector2 circleCenter, List<Vector2> vertices)
        {
            int result = -1;
            float minDistance = float.MaxValue;

            for (int i = 0; i < vertices.Count; i++)
            {
                Vector2 v = vertices[i];
                float distance = Vector2.Distance(v, circleCenter);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }

            return result;
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
