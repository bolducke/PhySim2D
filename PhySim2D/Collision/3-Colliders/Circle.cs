using PhySim2D.Tools;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("PhysicEngine.UI")]
[assembly: InternalsVisibleTo("PhysicEngine.UnitTest")]
namespace PhySim2D.Collision.Colliders
{
    [DataContract]
    internal class Circle : Collider
    {
        [DataMember(Order = 0)]
        public KVector2 LPosition { get; set; }

        [DataMember(Order = 1)]
        public float Radius { get; set; }

        public Circle(KVector2 pos, float radius)
        {
            LPosition = pos;
            Radius = radius;
            Type = ColliderType.CIRCLE;
            ComputeProperties();
        }

        public override void ComputeProperties()
        {
            Centroid = LPosition;
        }

        public override AABB ComputeAABB()
        {
            KVector2 offset = new KVector2(Radius);
            return new AABB(Transform.TransformPointLW(LPosition) - offset,Transform.TransformPointLW(LPosition) + offset);
        }

        public override KVector2 ComputeSupport(KVector2 wDirN)
        {
            return wDirN * Radius + Transform.TransformPointLW(LPosition);
        }
    }
}
