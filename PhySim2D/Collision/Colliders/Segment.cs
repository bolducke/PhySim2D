using PhySim2D.Tools;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Colliders
{
    [DataContract]
    internal class Segment : Collider
    {
        [DataMember]
        public KVector2 LStart { get; set; }

        [DataMember]
        public KVector2 LDir { get; set; }

        public Segment(KVector2 start, KVector2 end)
        {
            this.LStart = start;
            this.LDir = end - start;
            Type = ColliderType.SEGMENT;
            ComputeProperties();
        }

        public KVector2 RetrievePoint(float t)
        {
            return LDir * t + LStart;
        }

        public override AABB ComputeAABB()
        {
               KVector2.Extremity(
                new List<KVector2>
                {
                    Transform.TransformPointLW(LStart),
                    Transform.TransformPointLW(LDir + LStart)
                },
                out KVector2 min,
                out KVector2 max);

            return new AABB(min, max);
        }


        public override void ComputeProperties()
        {
            Centroid = RetrievePoint(0.5f);
        }

        public override KVector2 ComputeSupport(KVector2 dir)
        {
            KVector2 bestVertex = Transform.TransformPointLW(LStart);
            double bestVertexProj = dir * bestVertex;

            KVector2 vTrans = Transform.TransformPointLW(LStart + LDir);
            double proj = dir * vTrans;

            if (proj > bestVertexProj)
            {
                bestVertexProj = proj;
                bestVertex = vTrans;
            }

            return bestVertex;
        }

        public override string ToString()
        {
            return $" { LStart.ToString() } ; { RetrievePoint(1f).ToString() } >";
        }
    }
}
