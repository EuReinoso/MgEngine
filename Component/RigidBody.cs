using Microsoft.Xna.Framework;

namespace MgEngine.Component
{
    public class RigidBody
    {
        public enum ShapeType
        {
            Rect,
            Circle,
            Polygon
        }

        public Vector2 Velocity;

        public float Rotation;
        public float RotationVelocity;
        public float Mass;
        public float Restituition;
        public float Area;

        public bool IsStatic;
        public bool IsBody;

        public readonly ShapeType Type;

        public RigidBody(ShapeType type, float rotation = 0, float rotationVelocity = 0, float mass = 50, float restituition = 0, float area = 0, bool isStatic = false)
        {
            Type = type;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Mass = mass;
            Restituition = restituition;
            Area = area;
            IsStatic = isStatic;
        }
    }
}
