using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhySim2D.Tools;

namespace PhySim2D.UnitTest.Tools
{
    [TestClass]
    public class VerticesUT
    {
        [TestMethod]
        public void TestVerticesAreaRect()
        {
            KVertices rect = new KVertices
            {
                new KVector2(0, 1),
                new KVector2(1, 1),
                new KVector2(1, 0),
                new KVector2(0, 0)
            };

            Assert.AreEqual(1f, rect.Area());
        }

        [TestMethod]
        public void TestVerticesAreaTriangle()
        {
            KVertices tri = new KVertices
            {
                new KVector2(0, 0),
                new KVector2(1, 1),
                new KVector2(2, 0)
            };

            Assert.AreEqual(1f, tri.Area());
        }

        [TestMethod]
        public void TestVerticesIsConvex1()
        {
            KVertices rect = new KVertices
            {
                new KVector2(0, 1),
                new KVector2(1, 1),
                new KVector2(1, 0),
                new KVector2(0, 0)
            };

            Assert.AreEqual(true, rect.IsConvex());
        }

        [TestMethod]
        public void TestVerticesIsConvex2()
        {
            KVertices poly = new KVertices
            {
                new KVector2(0, 1),
                new KVector2(0.5f, 0.5f),
                new KVector2(1, 1),
                new KVector2(1, 0),
                new KVector2(0,0)
            };

            Assert.AreEqual(false, poly.IsConvex());
        }

        [TestMethod]
        public void TestVerticesIsConvex3()
        {
            KVertices poly = new KVertices
            {
                new KVector2(0, 0),
                new KVector2(0f, 1f),
                new KVector2(1, 1),
                new KVector2(1.5f, 1.5f),
                new KVector2(1, 1),
                new KVector2(1,0)
            };

            Assert.AreEqual(false, poly.IsConvex());
            Assert.AreEqual(4, poly.Count);

        }

        [TestMethod]
        public void TestTemp()
        {
            KVertices rect = new KVertices
            {
                new KVector2(1, 3),
                new KVector2(2, 2),
                new KVector2(1, 1),
                new KVector2(0,0),
                new KVector2(-1,1),
                new KVector2(-2,2),
                new KVector2(-1,3 ),
                new KVector2(0,4),
            };

            KVector2 temp = new KVector2();
            foreach (KVector2 v in rect)
            {
                temp += v;

            }

            KVector2 actual = temp * 1 / rect.Count;
            KVector2 expected = new KVector2(0, 2);

            Assert.AreEqual(expected, actual);
        }
    }
}
