using PhySim2D.Collision.Colliders;
using System;
using System.Collections.Generic;

namespace PhySim2D.Collision.Broadphase.Hierarchical_Grids
{
    struct HCell
    {
        public Collider Collider { get; set; }
        int TimeStamp { get; set; }

        public override bool Equals(object obj) => Collider.Equals(obj);

        public override int GetHashCode()
        {
            Int32 hashCode = 754720349;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Collider>.Default.GetHashCode(Collider);
            return hashCode;
        }
    }
}
