using PhySim2D.Sim;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace PhySim2D.Tools
{

    [DebuggerDisplay("Count = {Count} Vertices = {ToString()}")]
    internal class KVertices : List<KVector2>
    {
        //TODO: Tester si le polygon est bien construit pendant la construction des sommets
        public bool ValidPolygon()
        {
            if (Area() < Config.ValidAreaPolygon)
                throw new Exception("Area is too small");

            if (!IsConvex())
                throw new Exception("Polygon is not convex");

            if (!IsCounterClockwise())
                Reverse();

            return true;
        }


        public bool IsCounterClockwise()
        {
            if (Count < 3)
                return false;

            return (SignedArea() > 0);
        }

        public bool IsConvex()
        {
            if (Count < 3)
                return false;

            if (Count == 3)
                return true;

            //TODO: A ameliorer
            double oldValue = 0;
            bool convex = true;

            for (int i = 0; i < Count; i++)
            {
                int secondIndex = i + 1 < Count ? i + 1 : 0;
                int thirdIndex = secondIndex + 1 < Count ? secondIndex + 1 : 0;

                KVector2 edgeA = this[secondIndex] - this[i];
                KVector2 edgeB = this[thirdIndex] - this[secondIndex];

                double nextValue = edgeA % edgeB;

                if(KMath.AlmostEquals(nextValue, 0,Config.EpsilonsDouble))
                {
                    if(this[i] == this[thirdIndex])
                    {
                        //Order is important here
                        Remove(this[thirdIndex]);
                        Remove(this[secondIndex]); 
                    }
                    else
                    {
                        Remove(this[secondIndex]);
                    }

                    i = i - 1 > 0 ? i - 1 : Count;
                    continue;
                }

                convex &= nextValue * oldValue >= 0;

                oldValue = nextValue;
            }
        
            if ( Count < 3)
                return false;

            return convex;
        }

        internal  double SignedArea()
        {
            if (Count < 3)
                return 0;

            double area = 0;

            for (int i = 0; i < Count; i++)
            {
                int j = i + 1 < Count ? i + 1 : 0;

                KVector2 vCurr = this[i];
                KVector2 vNext = this[j];

                area += vCurr.X * vNext.Y;
                area -= vCurr.Y * vNext.X;

            }

            area = area / 2.0f;

            return area;
        }

        public double Area()
        {
            double area = SignedArea();
            return area < 0 ? -area : area;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                builder.Append(this[i]);
                if (i < Count - 1)
                {
                    builder.Append(" ");
                }
            }
            return builder.ToString();
        }

        public static explicit operator PointF[] (KVertices vertices)
        {
            PointF[] points = new PointF[vertices.Count];
            int i = 0;
            vertices.ForEach((v) =>
            {
                points[i] = new PointF((float) v.X,(float) v.Y);
                i++;
            });
            return points;
        }
    }
}
