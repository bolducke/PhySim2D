using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhySim2D.Tools;

namespace PhySim2D.UnitTest.Tools

{
    [TestClass]
    public class TransformUT
    {

        [TestMethod]
        public void TestPointRotation()
        {
            KVector2 point = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI / 2.0), new KVector2(1));

            Assert.AreEqual(new KVector2(0, 1), tx.TransformPointLW(point));
        }

        [TestMethod]
        public void TestPointRotation1()
        {
            KVector2 point = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI / 2.0), new KVector2(1));

            Assert.AreEqual(new KVector2(0,-1), tx.TransformPointWL(point));
        }

        [TestMethod]
        public void TestPointRotation2()
        {
            KVector2 point = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI / 2.0), new KVector2(1));


            Assert.AreEqual(new KVector2(1, 0), tx.TransformPointWL(tx.TransformPointLW(point)));
        }



        [TestMethod]
        public void TestPointTranslation()
        {
            KVector2 point = new KVector2(2, 3);
            KTransform tx = new KTransform(new KVector2(-5,-2), 0, new KVector2(1));

            Assert.AreEqual(new KVector2(-3, 1), tx.TransformPointLW(point));
        }


        [TestMethod]
        public void TestPointScale()
        {
            KVector2 point = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, 0, new KVector2(5));

            Assert.AreEqual(new KVector2(5,0), tx.TransformPointLW(point));
        }

        [TestMethod]
        public void TestDir()
        {
            KVector2 dir = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI), new KVector2(5));

            Assert.AreEqual(new KVector2(-5, 0), tx.TransformDirLW(dir));
        }

        [TestMethod]
        public void TestDir1()
        {
            KVector2 dir = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, 0, new KVector2(5,4));

            Assert.AreEqual(new KVector2(5, 0), tx.TransformDirLW(dir));
        }

        [TestMethod]
        public void TestNormal()
        {
            KVector2 dir = new KVector2(1, 0);
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI), new KVector2(5));
            
            Assert.AreEqual(tx.TransformNormalLW(dir), new KVector2(-1, 0));
        }

        [TestMethod]
        public void TestWorldToLocal()
        {
            KVector2 point = new KVector2(31, -4);
            KTransform tx = new KTransform(KVector2.One,(System.Math.PI /2.0), new KVector2(5));

            Assert.AreEqual(new KVector2(-1, -6), tx.TransformPointWL(point));
        }

        [TestMethod]
        public void TestLocalToWorld()
        {
            KVector2 point = new KVector2(-1, -6);
            KTransform tx = new KTransform(KVector2.One, (System.Math.PI /2), new KVector2(5));

            Assert.AreEqual(new KVector2(-29, 6), tx.TransformPointLW(point));
        }

        [TestMethod]
        public void TestLWAndWL()
        {
            KTransform tx = new KTransform(KVector2.Zero, (float)(System.Math.PI / 2.0), new KVector2(1));


            KTransform.ComputeLocalToWorld(tx, out KMatrix3x3Opti A);
            KTransform.ComputeWorldToLocal(tx, out KMatrix3x3Opti IA);
            KMatrix3x3Opti mat = (A * IA);

            Assert.IsTrue((A * IA).IsIdentity);
        }
    }
}
