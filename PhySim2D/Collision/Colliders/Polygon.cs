using PhySim2D.Tools;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Colliders
{
    [DataContract]
    internal class Polygon : Collider
    {
        [DataMember(Order = 0)]
        protected KVector2[] _BoundingBoxRel;

        [DataMember(Order = 1)]
        public KVertices Vertices { get; set; }

        public Polygon(KVertices vertices)
        {
            this.Vertices = vertices;
            Type = ColliderType.POLYGON;
            ComputeProperties();
        }

        public override KVector2 ComputeSupport(KVector2 wDirN)
        {
            KVector2 bestVertex = KVector2.Zero;
            double bestVertexProj = float.MinValue;
            KVector2 lDir = Transform.TransformDirWL(wDirN);
            KVector2 lPos = Transform.TransformPointWL(Transform.Position);

            for (int i = 0; i < Vertices.Count; i++)
            {
                double proj = KVector2.Dot(lDir, Vertices[i] - lPos);

                if (proj > bestVertexProj)
                {
                    bestVertexProj = proj;
                    bestVertex = Vertices[i];
                }
            }
            return Transform.TransformPointLW(bestVertex);
        }

        public Face ComputeFace(int vertexIndex)
        {
            int nextIndex = vertexIndex + 1 < Vertices.Count ? vertexIndex + 1 : 0;

            KVector2 start = Transform.TransformPointLW(Vertices[vertexIndex]);
            KVector2 end = Transform.TransformPointLW(Vertices[nextIndex]);
            KVector2 wNormal = KVector2.PerpCW(end - start);

            Face face = new Face()
            {
                WNormal = KVector2.Normalize(wNormal),
                WStart = start,
                WEnd = end
            };

            return face;
        }

        public override void ComputeProperties()
        {
            KVector2 min, max, center;

            max = min = center = Vertices[0];

            for (int i = 1; i < Vertices.Count; i++)
            {
                //OOB
                max = KVector2.Max(max, Vertices[i]);
                min = KVector2.Min(min, Vertices[i]);

                //Centroid
                center += Vertices[i];
            }

            //Centroid
            Centroid = center * (1 / Vertices.Count);

            //OOB
            _BoundingBoxRel = new KVector2[] {
                min,
                max,
                new KVector2(max.X, min.Y),
                new KVector2(min.X, max.Y)
            };

        }

        public override AABB ComputeAABB()
        {
            KVector2 max, min;
            max = min = Transform.TransformPointLW(_BoundingBoxRel[0]);

            for (int i = 1; i < _BoundingBoxRel.Length; i++)
            {
                KVector2 transV = Transform.TransformPointLW(_BoundingBoxRel[i]);
                max = KVector2.Max(max, transV);
                min = KVector2.Min(min, transV);
            }

            return new AABB(min, max);
        }
    }
}
