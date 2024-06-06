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

        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public float RotationVelocity { get; set; }
        public float Mass { get; set; }
        public float Restitution { get; set; }

        public bool IsStatic { get; set; }

        public readonly ShapeType Type;

        public RigidBody(ShapeType type, float rotation = 0, float rotationVelocity = 0, float mass = .02f, float restitution = 0.3f, bool isStatic = false)
        {
            Type = type;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Mass = mass;
            Restitution = restitution;
            IsStatic = isStatic;
        }

    }
}
