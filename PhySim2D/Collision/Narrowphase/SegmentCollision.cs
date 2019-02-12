using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Contacts;
using PhySim2D.Dynamics;
using PhySim2D.Tools;
using System;

namespace PhySim2D.Collision.Narrowphase
{
    class SegmentCollision
    {
        internal static bool SegmentVsSegment(ref Contact contact)
        {
            Segment a = (Segment)contact.FixtureA.Collider;
            Segment b = (Segment)contact.FixtureB.Collider;

            KVector2 n = KVector2.Normalize(KVector2.PerpCW(a.LDir));

            double penetration = n * (b.LStart - a.LStart);
            double t = penetration / (n * b.LDir);

            if (t < 0) return false;
            if (t > 1) return false;

            contact.Manifold.Count = 1;

            ContactPoint cp = new ContactPoint
            {
                WPosition = b.LStart - n * penetration,
                WPenetration = penetration
            };

            contact.Manifold.WNormal = n;
            contact.Manifold.ContactPoints[0] = cp;
            contact.Manifold.Count = 1;

            return true;
        }

        internal static bool SegmentVsPolygon(ref Contact contact)
        {
            Fixture.Swap(ref contact.FixtureA, ref contact.FixtureB);

            return PolygonCollisionSAT.PolygonVsSegment(ref contact);
        }

        internal static bool SegmentVsCircle(ref Contact contact)
        {
            Fixture.Swap(ref contact.FixtureA, ref contact.FixtureB);

            return CircleCollision.CircleVsSegment(ref contact);
        }

        internal static bool Collision(ref Contact contact)
        {
            switch (contact.FixtureB.Collider.Type)
            {
                case ColliderType.CIRCLE:
                    return SegmentVsCircle(ref contact);
                case ColliderType.SEGMENT:
                    return SegmentVsSegment(ref contact);
                case ColliderType.POLYGON:
                    return SegmentVsPolygon(ref contact);
                default:
                    throw new Exception("Collider type is not supported");
            }
        }
    }
}
