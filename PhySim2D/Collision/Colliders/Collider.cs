using PhySim2D.Tools;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Colliders
{
    public enum ColliderType
    {
        CIRCLE, SEGMENT, POLYGON
    }

    /// <summary>
    /// A collider is an abstract concept who facilate collision detection for a variety of geometry forms.
    /// </summary>
    [DataContract]
    internal abstract class Collider
    {
        /// <summary>
        /// The type of colliders
        /// </summary>
        [DataMember(Order = 0)]
        public ColliderType Type { get; protected set; }

        /// <summary>
        /// Methods returning the centroid of this collider
        /// </summary>
        /// <returns>The centroid of this collider</returns>
        [DataMember(Order = 1)]
        public KVector2 Centroid { get; protected set; }

        /// <summary>
        /// The transform parent associate to this collider
        /// </summary>
        [DataMember(Order = 2)]
        public KTransform Transform { get; set; }

        /// <summary>
        /// Methods returning a AABB box englobing the geometry form of this collider
        /// </summary>
        /// <returns>AABB box englobing the geometry form of this collider</returns>
        public abstract AABB ComputeAABB();

        public abstract KVector2 ComputeSupport(KVector2 dir);

        public abstract void ComputeProperties();

    }
}
