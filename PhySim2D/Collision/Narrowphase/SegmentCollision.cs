using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Contacts;
using PhySim2D.Dynamics;
using PhySim2D.Sim;
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

            KVector2 n = KVector2.Normalize(KVector2.PerpCW(a.LEnd - a.LStart));

            double penetration = n * (b.LStart - a.LStart);
            double t = penetration / (n * (b.LEnd - b.LStart));

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

        internal static bool SegmentVsCircle(ref Contact contact)
        {
            Segment a = (Segment) contact.FixtureA.Collider;
            Circle b = (Circle) contact.FixtureB.Collider;

            KVector2 lStart = b.Transform.TransformPointWL(a.Transform.TransformPointLW(a.LStart));
            KVector2 lEnd = b.Transform.TransformPointWL(a.Transform.TransformPointLW(a.LEnd));
            KVector2 lDirN = KVector2.Normalize(lEnd - lStart);

            //Closest point on segment to cercle center
            double u = (b.LPosition - lStart) * lDirN;
            double v = (b.LPosition - lEnd) * -lDirN;

            KVector2 lPos;
            KVector2 cSegDir;

            if (u < 0)
            {
                lPos = lStart;
                cSegDir = b.LPosition - lPos;

                contact.Manifold.WNormal = b.Transform.TransformNormalLW(cSegDir);
            }
            else if (v < 0)
            {
                lPos = lEnd;
                cSegDir = b.LPosition - lPos;

                contact.Manifold.WNormal = b.Transform.TransformNormalLW(cSegDir);
            }
            else
            {
                lPos = lStart + u * lDirN;
                cSegDir = b.LPosition - lPos;

                KVector2 lNormal = KVector2.PerpCW(lDirN);
                lNormal = lNormal * cSegDir * lNormal;

                contact.Manifold.WNormal = a.Transform.TransformNormalLW(lNormal);
            }

            if (cSegDir.LengthSquared() > 1 )
                return false;

            ContactPoint cp = new ContactPoint
            {
                WPosition = b.Transform.TransformPointLW(lPos),
                WPenetration = b.Transform.TransformDirLW(KVector2.Normalize(cSegDir) * (1 - cSegDir.Length())).Length()
            };

            contact.Manifold.ContactPoints[0] = cp;
            contact.Manifold.Count = 1;

            return true;
        }

        internal static bool SegmentVsPolygon(ref Contact contact)
        {
            Fixture.Swap(ref contact.FixtureA, ref contact.FixtureB);

            return PolygonCollisionSAT.PolygonVsSegment(ref contact);
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
