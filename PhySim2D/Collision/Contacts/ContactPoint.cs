using PhySim2D.Tools;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace PhySim2D.Collision.Contacts
{
    [DebuggerDisplay("Penetration : {WPenetration}, Position : {WPosition}")]
    [DataContract]
    internal class ContactPoint
    {
        [DataMember(Order = 0)]
        public KVector2 WPosition { get; set; }

        [DataMember(Order = 1)]
        public double WPenetration { get; set; }

        public ContactPoint()
        {
            WPosition = KVector2.Zero;
            WPenetration = 0f;
        }
    }
}
