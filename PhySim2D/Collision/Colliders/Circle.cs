using PhySim2D.Tools;
using System;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Colliders
{
    [DataContract]
    internal class Circle : Collider
    {
        [DataMember(Order = 0)]
        public KVector2 LPosition { get; set; }

        public Circle(KVector2 pos)
        {
            LPosition = pos;
            Type = ColliderType.CIRCLE;
            ComputeProperties();
        }

        public override void ComputeProperties()
        {
            Centroid = LPosition;
        }

        public override AABB ComputeAABB()
        {
            KVector2 max = new KVector2(ComputeSupport(KVector2.XPos).X, ComputeSupport(KVector2.YPos).Y);
            KVector2 min = new KVector2(ComputeSupport(KVector2.XNeg).X, ComputeSupport(KVector2.YNeg).Y);
            return new AABB(min,max);
        }

        public override KVector2 ComputeSupport(KVector2 wDirN)
        {
            KVector2 newPos = KVector2.Normalize(wDirN * Transform.MatLW) + LPosition;
        
            return Transform.TransformPointLW(newPos);
        }
    }
}
