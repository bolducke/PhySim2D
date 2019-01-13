using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhySim2D.Collision;
using PhySim2D.Collision.Narrowphase;
using PhySim2D.Dynamics;
using PhySim2D.Factories;
using PhySim2D.Tools;
using System;

namespace PhySim2D.UnitTest.Collision.Narrowphase
{
    [TestClass]
    public class PolygonCollisionTest
    {
        [TestMethod]
        public void TestOverlapSquareAndTrangle1()
        {
            KTransform t = new KTransform(KVector2.Zero, 0, KVector2.One);

            KVertices vA = new KVertices()
            {
                new KVector2(0f,0f + 2),
                new KVector2(1f,-2f + 2),
                new KVector2(-1f,-2f + 2),
           
            };
            KVertices vB = new KVertices()
            {
                new KVector2(0.5f,0.5f),
                new KVector2(0.5f,-0.5f),
                new KVector2(-0.5f,-0.5f),
                new KVector2(-0.5f,0.5f),
            };
            vA.Reverse();
            vB.Reverse();

            Rigidbody sqr = BodyFactory.CreatePolygon(t, new MassData(1, 1), vB);
            Rigidbody tri = BodyFactory.CreatePolygon(t, new MassData(1, 1), vA);

            Fixture fA = new Fixture(sqr, sqr.Colliders[0]);
            Fixture fB = new Fixture(tri, tri.Colliders[0]);

            Contact c = new Contact(fA,fB);
            Assert.AreEqual(CollisionDetection.Collision(ref c),true);
        }

        [TestMethod]
        public void TestOverlapSquareAndTrangle2()
        {
            KTransform t1 = new KTransform(KVector2.Zero, 0, KVector2.One);
            KTransform t2 = new KTransform(KVector2.Zero, 0f, KVector2.One);

            KVertices vA = new KVertices()
            {
                new KVector2(0f,1f),
                new KVector2(0.5f,0f),
                new KVector2(-0.5f,0f),
            };
            KVertices vB = new KVertices()
            {
                new KVector2(0.9f,0.5f),
                new KVector2(0.9f,-0.5f),
                new KVector2(-0.2f,-0.5f),
                new KVector2(-0.2f,0.5f),
            };
            vA.Reverse();
            vB.Reverse();

            Rigidbody tri = BodyFactory.CreatePolygon(t1, new MassData(1, 1), vA);
            Rigidbody sqr = BodyFactory.CreatePolygon(t2, new MassData(1, 1), vB);

            Fixture fA = new Fixture(sqr, sqr.Colliders[0]);
            Fixture fB = new Fixture(tri, tri.Colliders[0]);

            Contact c = new Contact(fA, fB);

            bool result = true;

            while (sqr.State.Transform.Rotation < 2 * Math.PI)
            {
                sqr.State.Transform.Rotation += 0.01f;
                if(!CollisionDetection.Collision(ref c))
                {
                    result = false;
                    break;
                }

            }

            Assert.AreEqual(true,result);
        }

        [TestMethod]
        public void TestOverlapSquareAnd8Poly()
        {
            KTransform t1 = new KTransform(KVector2.Zero, 0, KVector2.One);
            KTransform t2 = new KTransform(KVector2.Zero, 0, KVector2.One);

            KVertices vA = new KVertices()
            {
                new KVector2(-1f, -1f),
                new KVector2(-2.62f, 0.18f),
                new KVector2(-2.62f, 3.98f),
                new KVector2(-1f, 5.16f),
                new KVector2(1f, 5.16f),
                new KVector2(2.62f, 3.98f),
                new KVector2(2.62f, 0.18f),
                new KVector2(1f, -1f),
            };

            KVertices vB = new KVertices()
            {
                new KVector2(0.5f,-0.5f),
                new KVector2(-0.5f,-0.5f),
                new KVector2(-0.5f,-1.5f),
                new KVector2(0.5f,-1.5f),
                
            };
            vA.Reverse();

            Rigidbody sqr = BodyFactory.CreatePolygon(t1, new MassData(1, 1), vB);
            Rigidbody poly = BodyFactory.CreatePolygon(t2, new MassData(1, 1), vA);

            Fixture fA = new Fixture(sqr, sqr.Colliders[0]);
            Fixture fB = new Fixture(poly, poly.Colliders[0]);

            Contact c = new Contact(fA, fB);
            Assert.AreEqual(CollisionDetection.Collision(ref c), true);
        }


    }
}
