using PhySim2D.Collision.Contacts;
using PhySim2D.Sim;
using PhySim2D.Tools;
using System.Diagnostics;

namespace PhySim2D.Collision
{
    [DebuggerDisplay("Contact points : {Count}")]
    internal class ContactManifold
    {
        public KVector2 WNormal { get; set; }
        public float NormalImpulse { get; set; }
        public float TangentImpulse { get; set; }

        public ContactPoint[] ContactPoints { get; set; }

        public int Count { get; set; }
         
        public ContactManifold()
        {
            ContactPoints = new ContactPoint[Config.MaxManifoldPoints];
            Count = 0;
        }
    }
}
