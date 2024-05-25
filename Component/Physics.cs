using MgEngine.Shape;
using MgEngine.Time;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Component
{
    public class Physics
    {
        #region Variables
        public float Gravity = 10;

        private List<RigidBody> _bodyList;
        #endregion

        #region Constructor
        public Physics()
        {
            _bodyList = new List<RigidBody>();
        }
        #endregion

        #region Methods
        public void Add(RigidBody body)
        {
            _bodyList.Add(body);
        }

        public void Remove(RigidBody body) 
        { 
            _bodyList.Remove(body);
        }

        public void Clear()
        {
            _bodyList.Clear();
        }

        public void Update(float dt)
        {
            for (int i = 0; i < _bodyList.Count - 1; i++)
            {
                var b1 = _bodyList[i];

                for (int j = i + 1; j < _bodyList.Count; j++)
                {
                    UpdateMove(j);

                    var b2 = _bodyList[j];

                    if (b1.IsStatic && b2.IsStatic)
                        continue;

                    if (Collide(b1, b2, out Vector2 normal, out float depth))
                    {
                        if (b1.IsStatic)
                        {
                            Move(j, normal * depth);
                        }
                        else if (b2.IsStatic)
                        {
                            Move(i, -normal * depth);
                        }
                        else
                        {
                            Move(j, normal * depth / 2f);
                            Move(i, -normal * depth / 2f);
                        }

                        ResolveCollision(b1, b2, normal * dt);
                    }

                }
            }
        }

        private bool Collide(object b1, object b2, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = 0;

            var rb1 = (RigidBody)b1;
            var rb2 = (RigidBody)b2;

            if (rb1.Type is RigidBody.ShapeType.Circle)
            {
                if (rb2.Type is RigidBody.ShapeType.Circle)
                    return Circle.CollideCircle((Circle)b1, (Circle)b2, out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Polygon)
                    return Polygon.CollideCircleToPolygon((Circle)b1, (Polygon)b2, out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Rect)
                    return Polygon.CollideCircleToPolygon(((Circle)b1).Pos, ((Circle)b1).Radius, ((Rect)b2).Pos, ((Rect)b2).Vertices.ToList(), out normal, out depth);
            }

            if (rb1.Type is RigidBody.ShapeType.Polygon)
            {
                if (rb2.Type is RigidBody.ShapeType.Polygon)
                    return Polygon.CollidePolygon((Polygon)b1, (Polygon)(b2), out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Circle)
                    return Polygon.CollideCircleToPolygon((Circle)b2, (Polygon)b1, out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Rect)
                    return Polygon.CollidePolygon(((Polygon)b1).Vertices, ((Rect)b2).Vertices.ToList(), out normal, out depth);
            }

            if (rb1.Type is RigidBody.ShapeType.Rect)
            {
                if (rb2.Type is RigidBody.ShapeType.Polygon)
                    return Polygon.CollidePolygon(((Polygon)b2).Vertices, ((Rect)b1).Vertices.ToList(), out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Circle)
                {
                    bool collide = Polygon.CollideCircleToPolygon(((Circle)b2).Pos, ((Circle)b2).Radius, ((Rect)b1).Pos, ((Rect)b1).Vertices.ToList(), out normal, out depth);

                    normal *= -1;

                    return collide;
                }
                if (rb2.Type is RigidBody.ShapeType.Rect)
                    return Polygon.CollidePolygon(((Rect)b1).Vertices.ToList(), ((Rect)b2).Vertices.ToList(), out normal, out depth);
            }

            return false;
        }

        public void ResolveCollision(RigidBody b1, RigidBody b2, Vector2 normal)
        {
            Vector2 relativeVelocity = b2.Velocity - b1.Velocity;

            if (Vector2.Dot(relativeVelocity, normal) > 0f)
                return;

            float e = MathF.Min(b1.Restitution, b2.Restitution);

            float j = -(1f + e) * Vector2.Dot(relativeVelocity, normal);
            j /= b1.Mass + b2.Mass;

            Vector2 impulse = j * normal;

            b1.Velocity -= impulse * b1.Mass;
            b2.Velocity += impulse * b2.Mass;
        }

        private void Move(int index, Vector2 movement)
        {
            var rb1 = _bodyList[index];

            if (rb1.Type is RigidBody.ShapeType.Circle)
                ((Circle)_bodyList[index]).Pos += movement;

            else if (rb1.Type is RigidBody.ShapeType.Polygon)
                ((Polygon)_bodyList[index]).Move(movement);

            else if (rb1.Type is RigidBody.ShapeType.Rect)
                ((Rect)_bodyList[index]).Pos += movement;
        }

        private void UpdateMove(int index)
        {
            var rb1 = _bodyList[index];

            if (rb1.Type is RigidBody.ShapeType.Circle)
                ((Circle)_bodyList[index]).Pos += rb1.Velocity;

            else if (rb1.Type is RigidBody.ShapeType.Polygon)
                ((Polygon)_bodyList[index]).Move(rb1.Velocity);

            else if (rb1.Type is RigidBody.ShapeType.Rect)
                ((Rect)_bodyList[index]).Pos += rb1.Velocity; ;
        }

        #endregion
    }
}
