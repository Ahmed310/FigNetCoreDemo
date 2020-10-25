using System;
using System.Numerics;

namespace GameCommon.Physics
{
    public struct AABB
    {
        public float Width;
        public float Height;
        public Vector2 Center;
        public Vector2 TopLeft;

        public AABB(Vector2 center, float width, float height)
        {
            this.Center = center;
            this.Width = width;
            this.Height = height;
            TopLeft = new Vector2(center.X + width / 2f, center.Y - height / 2f);
        }
        /// <summary>
        /// Rect-Rect Collision
        /// </summary>
        /// <param name="aABB"></param>
        /// <returns></returns>
        public bool Contains(ref AABB aABB)
        {
            bool collision = false;

            if (TopLeft.X + Width >= aABB.TopLeft.X &&
                TopLeft.X <= aABB.TopLeft.X + aABB.Width &&
                TopLeft.Y + Height >= aABB.TopLeft.Y &&
                TopLeft.Y <= aABB.TopLeft.Y + aABB.Height)
            {
                collision = true;
            }

            return collision;
        }

        /// <summary>
        /// Circle-Rect Collision
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public bool Contains(ref Vector2 center, float radius)
        {
            float tmpX = center.X;
            float tmpY = center.Y;

            if (center.X < TopLeft.X)
            {
                tmpX = TopLeft.X;
            }
            else if (center.X > TopLeft.X + Width)
            {
                tmpX = TopLeft.X + Width;
            }

            if (center.Y < TopLeft.Y)
            {
                tmpY = TopLeft.Y;
            }
            else if (center.Y > TopLeft.Y + Height)
            {
                tmpY = TopLeft.Y + Height;
            }

            double distX = center.X - tmpX;
            double distY = center.Y - tmpY;
            double distance = Math.Sqrt((distX * distX) + (distY * distY));

            // if (distance <= radius)
            return distance <= radius;

        }
    }
}
