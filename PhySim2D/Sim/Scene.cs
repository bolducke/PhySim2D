using PhySim2D.Dynamics;
using System;
using System.Collections.Generic;
using PhySim2D.Tools;
using PhySim2D.Collision;
using PhySim2D.Dynamics.Forces;
using System.Runtime.Serialization;
using PhySim2D.Factories;
using PhySim2D.Collision.Colliders;
using PhySim2D.Collision.Broadphase;
using PhySim2D.Collision.Narrowphase;
using PhySim2D.Collision.Broadphase.Brute;
using PhySim2D.Dynamics.Integrator;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PhysicEngine.UI")]
namespace PhySim2D.Sim
{

    [DataContract]
    public class Scene
    {
        [DataMember]
        internal List<Rigidbody> Bodies { get; private set; } = new List<Rigidbody>();

        internal BroadphaseManager broadphaseManager = new ImprovedBruteBroadphase();
        internal NarrowphaseManager narrowphaseManager = new NarrowphaseManager();
        internal IRigidbodyIntegrator Integrator;
        internal event EventHandler<ContactInCollisionEventArgs> ContactsInCollision;

        Rigidbody rect;

        
        public Scene()
        {
            KTransform t1 = new KTransform(new KVector2(0f,5f), 1f, new KVector2(2));
            KTransform t2 = new KTransform(new KVector2(0, -5.5f), 0f, new KVector2(1));
            KTransform t3 = new KTransform(new KVector2(0f, 2.1f), 0f, new KVector2(1));
            List<Collider> ss = new List<Collider>
            {
                new Circle(new KVector2(0,0), 0.5f),
                new Circle(new KVector2(10,4), 0.5f)
            };
            //Vertices v1 = new Vertices()
            //{
            // new KVector2(-1f, -1f),
            // new KVector2(-2.62f, 0.18f),
            // new KVector2(-2.62f, 3.98f),
            // new KVector2(-1f, 5.16f),
            // new KVector2(1f, 5.16f),
            // new KVector2(2.62f, 3.98f),
            // new KVector2(2.62f, 0.18f),
            // new KVector2(1f, -1f),
            //};
            //v1.Reverse();
            KVertices v1 = new KVertices()
            {
                new KVector2(0.5f,0.5f),
                new KVector2(-0.5f,0.5f),
                new KVector2(-0.5f,-0.5f),
                new KVector2(0.5f,-0.5f),
            };
            //Vertices v1 = new Vertices()
            //{
            //    new KVector2(-0.4330127018922f,-0.25f),
            //    new KVector2(0.4330127018922f,-0.25f),
            //    new KVector2(0f,0.5f),
            //};
            //v1.Reverse();
            //Vertices v = new Vertices()
            //{
            //    new KVector2(0.5f,0.5f),
            //    new KVector2(-0.5f,0.5f),
            //    new KVector2(-0.5f,-0.5f),
            //    new KVector2(0.5f,-0.5f),
            //};
            KVertices v = new KVertices()
            {
                new KVector2(5f,5f),
                new KVector2(-5f,5f),
                new KVector2(-5f,-5f),
                new KVector2(5f,-5f),
            };

            Rigidbody s = BodyFactory.CreatePolygon(t1, new MassData(3, 3), v1);
            Rigidbody s2 = BodyFactory.CreatePolygon(t3, new MassData(1, 3), v1);
            //Rigidbody c = BodyFactory.CreateCircleBody(1f, new MassData(1, 1), t1);
            //Rigidbody seg = BodyFactory.CreateSegmentBody(new KVector2(-1, 0), new KVector2(1, 0), new MassData(1,1), t2);
            rect = BodyFactory.CreatePolygon(t2, new MassData(0,0), v);
            Bodies.Add(s);
            Bodies.Add(rect);
            Bodies.Add(s2);
            s.AddImpulseAtWPos(new KVector2(3f, 0), s.State.Transform.Position);

            s.State.ForceGenerators.Add("G", new GravityHMR(new KVector2(0, -2)));
            s2.State.ForceGenerators.Add("G", new GravityHMR(new KVector2(0,-2)));

            Integrator = new RK4();
        }

        public void DoStep(float step)
        {
            //Forces

            //Update Position and Velocity
            foreach (Rigidbody body in Bodies)
                Integrator.Integrate(body,step);

            //Broadphase
            List<Contact> pairs = broadphaseManager.SpotPotentialCollision(Bodies);

            //Narrowphase
            pairs = narrowphaseManager.SpotCollision(pairs);
            InContact(this, new ContactInCollisionEventArgs(pairs));
            //Constraints

            //DistanceConstraintSI c = new DistanceConstraintSI(Bodies[0], Bodies[1], 20);
            //c.SolveConstraints();

            //Constraint solver
            ContactSolverSI.SolveContact(pairs);
        }

        internal void InContact(object sender, ContactInCollisionEventArgs e)
        {
            ContactsInCollision?.Invoke(sender, e);
        }

    }
}
