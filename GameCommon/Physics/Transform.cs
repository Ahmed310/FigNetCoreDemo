using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameCommon.Physics
{
    public struct Transform
    {
        public Vector2 p;
        public float r;     // angle in degree

        public void Set(Vector2 position, float angle)
        {
            p = position;
            r = angle;
        }
        public void SetIdentity()
        {
            p.X = 0;
            p.Y = 0;
            r = 0;
        }
    }
}
