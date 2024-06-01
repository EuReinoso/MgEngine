﻿using Microsoft.Xna.Framework;

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
        public float Restitution;

        public bool IsStatic;

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
