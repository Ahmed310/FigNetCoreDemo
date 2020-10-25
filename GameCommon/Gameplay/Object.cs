using GameCommon.Physics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameCommon.Gameplay
{
    public abstract class Object
    {
        public Transform Transform;
        public AABB BoundingBox;
        public float Radius { get; set; }

        /// <summary>
        /// circle collision between two objects
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public bool IsCircleCollision(Object @object)
        {
            return Vector2.Distance(Transform.p, @object.Transform.p) <= Radius + @object.Radius;
        }

        /// <summary>
        /// AABB collision between 2 objects
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public bool IsAABBCollision(Object @object)
        {
            return BoundingBox.Contains(ref @object.BoundingBox);
        }

        /// <summary>
        /// circle of pass object is checked with AABB of object
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public bool IsAABBCircleCollision(Object @object)
        {
            return false;
        }
    }
}
