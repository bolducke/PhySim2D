using PhySim2D.Collision.Broadphase;
using PhySim2D.Collision.Colliders;
using PhySim2D.Dynamics.Forces;
using PhySim2D.Dynamics.Integrator;
using PhySim2D.Tools;
using PhySim2D.Sim;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("PhysicEngine.UnitTest")]
namespace PhySim2D.Dynamics
{
    [DataContract]
    internal class Rigidbody : IBroadphaseEntity
    {
        [DataMember]
        private List<Collider> _Colliders;

        internal List<Collider> Colliders
        {
            get
            {
                return _Colliders;
            }

            private set
            {
                _Colliders = value;
                foreach(Collider shape in _Colliders)
                {
                    shape.Transform = State.Transform;
                }
            }
        }

        [DataMember]
        internal MassData MassData { get; private set; }
    
        [DataMember]
        internal PhysicMateriel Materiel { get; private set; }

        [DataMember]
        internal State State { get; private set; }

        [DataMember]
        private IRigidbodyIntegrator Integrator { get; set; }

        public Rigidbody(List<Collider> shapes, MassData massData, KTransform transform)
        {
            State = new State
            {
                Transform = transform
            };
            Colliders = shapes;
            MassData = massData;
            MassData.ComputeCenterOfMass(Colliders);
            Materiel = new PhysicMateriel(0.5f,1f);
        }

        public Rigidbody (Collider shape, MassData massData, KTransform transform) :
        this(new List<Collider> { shape }, massData, transform)
        { }

        public void AddForce(KVector2 force)
        {
            State.ForcesAccumulate += force;
        }

        public void AddTorque(double torque)
        {
            State.TorquesAccumulate += torque;
        }

        public void AddForceAtWPos(KVector2 wforce, KVector2 wPos)
        {

            AddTorque((wPos - MassData.CenterOfMass) % wforce);
            AddForce(wforce);
        }

        public void AddImpulseAtWPos(KVector2 wImpulse, KVector2 wPos)
        {
            KVector2 transformedCM = State.Transform.TransformPointLW(MassData.CenterOfMass);
            State.AngVelocity += (wPos - transformedCM) % wImpulse * MassData.InvInertia;
            State.Velocity += wImpulse * MassData.InvMass;
        }

        public void AddImpulseAtRelPosToCenter(KVector2 wImpulse, KVector2 relPos)
        {
            State.AngVelocity += relPos % wImpulse * MassData.InvInertia;
            State.Velocity += wImpulse * MassData.InvMass;
        }

        //TODO: Un peu weird comme approche. A revoir!
        internal void Acceleration(State state, float h, out KVector2 acc, out double angAcc)
        {

            KVector2 force = State.ForcesAccumulate;
            double torque = State.TorquesAccumulate;

            KVector2 tempForce;
            double tempTorque;

            foreach (KeyValuePair<string, IForceGenerator> forceGen in State.ForceGenerators)
            {
                (tempForce, tempTorque) = forceGen.Value.UpdateForce(MassData, Materiel, state, h);
                force += tempForce;
                torque += tempTorque;
            }

            acc = force * MassData.InvMass;
            angAcc = torque * MassData.InvInertia;
        }
    }
}
