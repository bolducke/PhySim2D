using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Contacts;
using PhySim2D.Dynamics;
using PhySim2D.Tools;
using System;

namespace PhySim2D.Collision.Narrowphase
{
    class CircleCollision
    {
        private static bool CircleVsCircle(ref Contact contact)
        {
            Circle a = (Circle)contact.FixtureA.Collider;
            Circle b = (Circle)contact.FixtureB.Collider;

            KVector2 cA = a.Transform.TransformPointLW(a.LPosition);
            KVector2 cB = b.Transform.TransformPointLW(b.LPosition);

            KVector2 dir = cA - cB;
            float r = a.Radius + b.Radius;

            if (dir.LengthSquared() < r * r)
            {
                ContactPoint cp = new ContactPoint
                {
                    WPosition = (cA + cB) * 0.5f,
                    WPenetration = dir.Length()
                };

                contact.Manifold.WNormal = KVector2.Normalize(dir);
                contact.Manifold.ContactPoints[0] = cp;
                contact.Manifold.Count = 1;

                return true;
            }

            return false;
        }

        public static bool CircleVsSegment(ref Contact contact)
        {
            Circle a = (Circle)contact.FixtureA.Collider;
            Segment b = (Segment)contact.FixtureB.Collider;

            KVector2 start = b.Transform.TransformPointLW(b.LStart);
            KVector2 dir = b.Transform.TransformDirLW(b.LDir);
            KVector2 end = dir + start;
            KVector2 positionA = a.Transform.TransformPointLW(a.LPosition);

            //Closest point on segment to cercle center
            double u = (positionA - start) * dir;
            double v = (start + dir - positionA) * dir;

            KVector2 wPos;

            if (u < 0)
            {
                wPos = start;
            }
            else if (v < 0)
            {
                wPos = end;
            }
            else
            {
                wPos = 1/dir.LengthSquared() * (u * start + v * end);
            }

            KVector2 cSegDir = positionA - wPos;

            if (cSegDir.LengthSquared() > a.Radius + a.Radius)
                return false;

            ContactPoint cp = new ContactPoint
            {
                WPosition = wPos,
                WPenetration = a.Radius - cSegDir.Length(),
            };

            contact.Manifold.WNormal = KVector2.Normalize(dir);
            contact.Manifold.Count = 1;

            return true;

        }

        public static bool CircleVsPolygon(ref Contact contact)
        {
            Fixture.Swap(ref contact.FixtureA, ref contact.FixtureB);

            return PolygonCollisionSAT.PolygonVsCircle(ref contact);
        }

        public static bool Collision(ref Contact contact)
        {
            switch (contact.FixtureB.Collider.Type)
            {
                case ColliderType.CIRCLE:
                    return CircleVsCircle(ref contact);
                case ColliderType.SEGMENT:
                    return CircleVsSegment(ref contact);
                case ColliderType.POLYGON:
                    return CircleVsPolygon(ref contact);
                default:
                    throw new Exception("Collider type is not supported");
            }

        }
    }
}
