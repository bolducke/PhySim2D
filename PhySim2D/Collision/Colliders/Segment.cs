using System.Collections.Generic;
using System.Runtime.Serialization;
using PhySim2D.Tools;

namespace PhySim2D.Collision.Colliders
{
    [DataContract]
    internal class Segment : Collider
    {
        [DataMember]
        public KVector2 LStart { get; set; }

        [DataMember]
        public KVector2 LEnd { get; set; }

        public Segment(KVector2 start, KVector2 end)
        {
            this.LStart = start;
            this.LEnd = end;
            Type = ColliderType.SEGMENT;
            ComputeProperties();
        }

        public override AABB ComputeAABB()
        {
            KVector2 max = new KVector2(ComputeSupport(KVector2.XPos).X,ComputeSupport(KVector2.YPos).Y);
            KVector2 min = new KVector2(ComputeSupport(KVector2.XNeg).X, ComputeSupport(KVector2.YNeg).Y);

            return new AABB(min, max);
        }


        public override void ComputeProperties()
        {
            Centroid = (LStart + LEnd)/2;
        }

        public override KVector2 ComputeSupport(KVector2 wDirN)
        {
            KVector2 lDir = Transform.TransformDirWL(wDirN);

            double projStart = lDir * LStart;
            double projEnd = lDir * LEnd;

            if (projStart > projEnd)
                return Transform.TransformPointLW(LStart);
                
            return Transform.TransformPointLW(LEnd);
        }

        public override string ToString()
        {
            return $" { LStart.ToString() } ; { LEnd.ToString() } >";
        }
    }
}
