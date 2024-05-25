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

        public void Update(float dt)
        {
            for (int i = 0; i < _bodyList.Count - 1; i++)
            {
                var b1 = _bodyList[i];

                for (int j = i + 1; j < _bodyList.Count; j++)
                {
                    var b2 = _bodyList[j];

                    if (Collide(b1, b2, out Vector2 normal, out float depth))
                    {
                        Resolve(j, normal * depth / 2);
                        Resolve(i, -normal * depth / 2);
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
            }

            if (rb1.Type is RigidBody.ShapeType.Polygon)
            {
                if (rb2.Type is RigidBody.ShapeType.Polygon)
                    return Polygon.CollidePolygon((Polygon)b1, (Polygon)(b2), out normal, out depth);
                if (rb2.Type is RigidBody.ShapeType.Circle)
                    return Polygon.CollideCircleToPolygon((Circle)b2, (Polygon)b1, out normal, out depth);
            }

            return false;
        }

        private void Resolve(int index, Vector2 movement)
        {
            var rb1 = _bodyList[index];

            if (rb1.Type is RigidBody.ShapeType.Circle)
                ((Circle)_bodyList[index]).Pos += movement;

            else if (rb1.Type is RigidBody.ShapeType.Polygon)
                ((Polygon)_bodyList[index]).Move(movement);
        }

        #endregion
    }
}
