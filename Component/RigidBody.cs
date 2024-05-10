using Microsoft.Xna.Framework;

namespace MgEngine.Component
{
    public class RigidBody
    {
        public Vector2 Velocity;
        public Vector2 Pos;

        public float Rotation;
        public float RotationVelocity;
        public float Mass;
        public float Restituition;
        public float Area;

        public bool IsStatic;

        public RigidBody(Vector2 pos, float rotation = 0, float rotationVelocity = 0, float mass = 50, float restituition = 0, float area = 0, bool isStatic = false)
        {
            Pos = pos;
            Rotation = rotation;
            RotationVelocity = rotationVelocity;
            Mass = mass;
            Restituition = restituition;
            Area = area;
            IsStatic = isStatic;
        }
    }
}
