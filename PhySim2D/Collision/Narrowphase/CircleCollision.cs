using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Contacts;
using PhySim2D.Dynamics;
using PhySim2D.Tools;
using System;

namespace PhySim2D.Collision.Narrowphase
{
    class CircleCollision
    {
        //FIXME: Bad CircleVsCircle test

        private static bool CircleVsCircle(ref Contact contact)
        {
            Circle a = (Circle)contact.FixtureA.Collider;
            Circle b = (Circle)contact.FixtureB.Collider;
            //In the local worl A is a unit circle and B is an ellipse

            KVector2 cB = a.Transform.TransformPointWL(b.Transform.TransformPointLW(b.LPosition));
            KVector2 axisA = a.Transform.TransformDirWL(b.Transform.TransformDirLW(KVector2.XPos));
            KVector2 axisB = a.Transform.TransformDirWL(b.Transform.TransformDirLW(KVector2.YPos));

            KTransform kTransform = new KTransform(cB, a.Transform.Rotation + b.Transform.Rotation, KVector2.One);
            KVector2 P = kTransform.TransformPointWL(a.LPosition);

            double t = Math.PI / 4f;

            KVector2 scale = new KVector2(axisA.Length(), axisB.Length());

            KVector2 potCp = new KVector2();

            for (int i = 0; i < 50; i++)
            {
                potCp = new KVector2(scale.X * Math.Cos(t), scale.Y * Math.Sin(t));

                double ex = (scale.X * scale.X) - (scale.Y * scale.Y) * Math.Pow(Math.Cos(t), 3) / scale.X;
                double ey = (scale.Y * scale.Y) - (scale.X * scale.X) * Math.Pow(Math.Sin(t), 3) / scale.Y;
                KVector2 e = new KVector2(ex, ey);

                KVector2 r = potCp - e;
                KVector2 q = P - e;
                double delta_c = r.Length() * Math.Asin((r.X * q.Y - r.Y * q.X) / (r.Length() * q.Length()));
                double delta_t = delta_c / Math.Sqrt(scale.LengthSquared() - potCp.LengthSquared());

                t += delta_t;
                t += Math.Min(Math.PI / 2, Math.Max(0, t));

            }

            KVector2 wPos = a.Transform.TransformPointLW(kTransform.TransformPointLW(potCp));
            KVector2 cDir = P - potCp;
            KVector2 wDir = b.Transform.TransformDirLW(KVector2.Normalize(cDir) * (1 - cDir.Length()));

            if (wDir.Length() >= 1)
                return false;

            ContactPoint cp = new ContactPoint
            {
                WPosition = wPos,
                WPenetration = wDir.Length()
            };

            contact.Manifold.WNormal = KVector2.Normalize(wDir);
            contact.Manifold.ContactPoints[0] = cp;
            contact.Manifold.Count = 1;

            return true;
        }

        public static bool CircleVsSegment(ref Contact contact)
        {
            Fixture.Swap(ref contact.FixtureA, ref contact.FixtureB);

            return SegmentCollision.SegmentVsCircle(ref contact);
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
