using PhySim2D.Tools;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Colliders
{
    /// <summary>
    /// AABB is a structure, represented by two points 
    /// in space, containing a more complex geometry form
    /// to reduce calculation time
    /// </summary>
    [DataContract]
    internal struct AABB
    {
        [DataMember]
        public KVector2 Min { get; set; }

        [DataMember]
        public KVector2 Max { get; set; }

        /// <summary>
        /// Constructor that create a box based on two points in space
        /// </summary>
        /// <param name="min">The point who its composants are the minimum of an AABB</param>
        /// <param name="max">The point who its composants are the maximum of an AABB</param>
        public AABB(KVector2 min, KVector2 max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Function who test if AABB are overlapping.
        /// </summary>
        /// <param name="a">The AABB a</param>
        /// <param name="b">The AABB b</param>
        /// <returns></returns>
        public static bool TryOverlap(AABB a, AABB b)
        {
            if (a.Max.X < b.Min.X || a.Min.X > b.Max.X) return false;
            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y) return false;
            
            return true;
        }

        public static explicit operator System.Drawing.RectangleF (AABB box)
        {
            return new System.Drawing.RectangleF((float) box.Min.X,(float) box.Min.Y,(float) (box.Max.X - box.Min.X),(float) (box.Max.Y - box.Min.Y));
        }
    }
}
