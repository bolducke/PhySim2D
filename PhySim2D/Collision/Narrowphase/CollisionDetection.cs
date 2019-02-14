using PhySim2D.Collision.Colliders;
using PhySim2D.Sim;
using PhySim2D.Tools;
using System;

namespace PhySim2D.Collision.Narrowphase
{
    internal static class CollisionDetection
    {
        public static bool Collision(ref Contact contact)
        {
            switch (contact.FixtureA.Collider.Type)
            {
                case ColliderType.CIRCLE:
                    return CircleCollision.Collision(ref contact);
                case ColliderType.SEGMENT:
                    return SegmentCollision.Collision(ref contact);
                case ColliderType.POLYGON:
                    return PolygonCollisionSAT.Collision(ref contact);
                default:
                    throw new Exception("Collider type is not supported");
            }
        }

        internal static void SeparationSAT(Polygon polygon, Collider collider, out int index, out double separation)
        {
            double bestDist = double.MinValue;
            int bestIndex = 0;
            for (int i = 0; i < polygon.Vertices.Count; i++)
            {
                Face face = polygon.ComputeFace(i);
                KVector2 s = collider.ComputeSupport(-face.WNormal);
                double dist = Face.DistanceFaceToPoint(face, s);

                if (dist >= bestDist + Config.EpsilonsFloat)
                {
                    //Optimisation: Stop looping early if a separating axis is found
                    //A positive separation mean that an axis was found
                    if (dist >= Config.EpsilonsFloat)
                    {
                        index = -1;
                        separation = Config.EpsilonsFloat;
                        return;
                    }

                    bestDist = dist;
                    bestIndex = i;
                }
            }

            index = bestIndex;
            separation = bestDist;
        }

        internal static void FindIncidentFace(Polygon poly, KVector2 wNormal, int index, out Face faceOutput)
        {
            int previousIndex = index - 1 < 0 ? poly.Vertices.Count - 1 : index - 1;

            Face faceSideLeft = poly.ComputeFace(previousIndex);
            Face faceSideRight = poly.ComputeFace(index);

            double left = KVector2.Dot(faceSideLeft.WNormal, wNormal);
            double right = KVector2.Dot(faceSideRight.WNormal, wNormal);

            if (left > right)
            {
                faceOutput = faceSideLeft;
            }
            else
            {
                faceOutput = faceSideRight;
            }
        }

        public static int ClipPointsToLine(KVector2 start, KVector2 end, KVector2 normal, double offSet, out KVector2[] output)
        {
            int nPoints = 0;

            output = new KVector2[2];

            double d0 = KVector2.Dot(normal, start) - offSet;
            double d1 = KVector2.Dot(normal, end) - offSet;

            if (d0 <= 0f)
            {
                output[nPoints++] = start;
            }

            if (d1 <= 0f)
            {
                output[nPoints++] = end;
            }

            if (d1 * d0 < 0f)
            {
                //Distance from start /(Lenght of line from start to end)
                double alpha = d0 / (d0 - d1);

                KVector2 point = KVector2.Lerp(start, end, alpha);

                output[nPoints++] = point;
            }

            return nPoints;
        }
    }
}
