using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Narrowphase;
//using PhySim2D.MathTools;
using PhySim2D.Sim;
using PhySim2D.Tools;

namespace PhySim2D.UnitTest.Collision.Narrowphase
{
    [TestClass]
    public class CollisionDetectionTest
    {
        [TestMethod]
        public void TestSeparationSAT1()
        {
            KTransform t1 = new KTransform(KVector2.Zero, 0, KVector2.One);
            KTransform t2 = new KTransform(KVector2.Zero, 0f, KVector2.One);

            KVertices vA = new KVertices()
            {
                new KVector2(0f,.5f),
                new KVector2(1f,2f),
                new KVector2(-1f,2f),
            };
            KVertices vB = new KVertices()
            {
                new KVector2(-1,-1),
                new KVector2(1,-1),
                new KVector2(1,1),
                new KVector2(-1,1),
            };

            Polygon tri = new Polygon(vA);
            tri.Transform = t1;
            Polygon sqr = new Polygon(vB);
            sqr.Transform = t2;

            CollisionDetection.SeparationSAT(tri, sqr, out int indexA, out double separationA);

            CollisionDetection.SeparationSAT(sqr, tri, out int indexB, out double separationB);

            Assert.IsFalse(separationA > Config.EpsilonsDouble);
            Assert.IsFalse(separationB > Config.EpsilonsDouble);

            Assert.AreEqual(new KVector2(0, .5f), tri.Vertices[indexA]);
            Assert.AreEqual(new KVector2(1, 1), sqr.Vertices[indexB]);
        }

        [TestMethod]
        public void TestClipLineToSegment1()
        {
            KVector2 start = new KVector2(-2, 3);
            KVector2 end = new KVector2(1, 0);

            KVector2 normal = new KVector2(-1, 0);

            CollisionDetection.ClipPointsToLine(start, end, normal, 0, out KVector2[] output);

            Assert.AreEqual(new KVector2(1, 0), output[0]);
            Assert.AreEqual(new KVector2(0, 1), output[1]);
        }

        [TestMethod]
        public void TestClipLineToSegment2()
        {
            KVector2 start = new KVector2(-2, 3);
            KVector2 end = new KVector2(1, 0);

            KVector2 normal = new KVector2(-1, 0);

            CollisionDetection.ClipPointsToLine(start, end, normal, 1, out KVector2[] output);

            Assert.AreEqual(new KVector2(1, 0), output[0]);
            Assert.AreEqual(new KVector2(-1, 2), output[1]);
        }

        [TestMethod]
        public void TestClipLineToSegment3()
        {
            KVector2 start = new KVector2(-2, 3);
            KVector2 end = new KVector2(1, 0);

            KVector2 normal = new KVector2(1, 0);

            CollisionDetection.ClipPointsToLine(start, end, normal, 0.5f, out KVector2[] output);

            Assert.AreEqual(new KVector2(-2, 3), output[0]);
            Assert.AreEqual(new KVector2(0.5f, 0.5f), output[1]);
        }


        [TestMethod]
        public void TestFindIncidentFace1()
        {
            KTransform t1 = new KTransform(KVector2.Zero, 0, KVector2.One);
            KTransform t2 = new KTransform(KVector2.Zero, 0f, KVector2.One);

            KVertices vA = new KVertices()
            {
                new KVector2(0f,.5f),
                new KVector2(1f,2f),
                new KVector2(-1f,2f),
            };
            KVertices vB = new KVertices()
            {
                new KVector2(-1,-1),
                new KVector2(1,-1),
                new KVector2(1,1),
                new KVector2(-1,1),
            };

            Polygon a = new Polygon(vA);
            a.Transform = t1;
            Polygon b = new Polygon(vB);
            b.Transform = t2;


            CollisionDetection.SeparationSAT(a, b, out int indexA, out double separationA);

            CollisionDetection.SeparationSAT(b, a, out int indexB, out double separationB);

            Polygon refPoly;
            Polygon incidentPoly;

            Face refFace;
            Face incidentFace;

            if (separationB > separationA + Config.EpsilonsFloat)
            {
                refPoly = b;
                refFace = b.ComputeFace(indexB);
                incidentPoly = a;
                CollisionDetection.FindIncidentFace(incidentPoly, -refFace.WNormal, indexA, out incidentFace);
            }
            else
            {
                refPoly = a;
                refFace = a.ComputeFace(indexA);
                incidentPoly = b;
                CollisionDetection.FindIncidentFace(incidentPoly, -refFace.WNormal, indexB, out incidentFace);
            }

            //Assert.AreEqual(new KVector2(-2, 3), output[0]);
            //Assert.AreEqual(new KVector2(0.5f, 0.5f), output[1]);
        }
    }
}
