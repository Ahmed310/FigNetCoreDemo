using System.Numerics;
using System.Collections.Generic;

namespace GameCommon.Gameplay
{
    public abstract class GameObject : Object
    {
        public float ProximityRadius { get; set; }
        public List<GameObject> ProximityList = new List<GameObject>();
        public abstract void UpdateProximityList();

        public bool IsProximity(GameObject gameObject)
        {
            return Vector2.DistanceSquared(this.Transform.p, gameObject.Transform.p) < (ProximityRadius);
        }
    }
}
