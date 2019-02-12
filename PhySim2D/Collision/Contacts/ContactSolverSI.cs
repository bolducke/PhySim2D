using PhySim2D.Dynamics;
using PhySim2D.Tools;
using PhySim2D.Sim;
using System;
using System.Collections.Generic;

namespace PhySim2D.Collision
{
    //TODO: Enregistrer limpulsion et lappliquer plus tard

    //TODO: Refaire le solver en se basant sur le model de Erin Catto combinant Si et block solver
    class ContactSolverSI
    {

        public static void SolveContact(List<Contact> contacts)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                AdjustVelocities(contacts[i]);

                AdjustPositions(contacts[i]);
            }

        }

#region Velocity Constraint
        private static void AdjustVelocities(Contact c)
        {
            Rigidbody bodyA = c.FixtureA.Body;
            Rigidbody bodyB = c.FixtureB.Body;



            for (int i = 0; i < c.Manifold.Count; i++)
            {

                KVector2 wPosition = c.Manifold.ContactPoints[i].WPosition;
                KVector2 rA = wPosition - bodyA.State.Transform.TransformPointLW(bodyA.MassData.CenterOfMass);
                KVector2 rB = wPosition - bodyB.State.Transform.TransformPointLW(bodyB.MassData.CenterOfMass);
                KVector2 wNormal = c.Manifold.WNormal;
                KVector2 relVitAB = bodyB.State.Velocity + rB % bodyB.State.AngVelocity  - bodyA.State.Velocity - rA % bodyA.State.AngVelocity;

                if (relVitAB * wNormal >= 0 )
                   break;

                ApplyVelocityChange(bodyA, bodyB, wNormal, wPosition, relVitAB, c.Manifold.Count);
            }
        }

        private static void ApplyVelocityChange(Rigidbody bodyA, Rigidbody bodyB, KVector2 wNormal, KVector2 wPosition, KVector2 relVitAB, int count)
        {
            KVector2 rA = wPosition - bodyA.State.Transform.TransformPointLW(bodyA.MassData.CenterOfMass);
            KVector2 rB = wPosition - bodyB.State.Transform.TransformPointLW(bodyB.MassData.CenterOfMass);

            double rAXwNormal = (bodyA.MassData.InvInertia * (rA % wNormal) * (rA % wNormal));
            double rBXwNormal = (bodyB.MassData.InvInertia * (rB % wNormal) * (rB % wNormal));

            double MassEff = bodyA.MassData.InvMass + bodyB.MassData.InvMass + rAXwNormal + rBXwNormal;
            double j = -(1 + Math.Min(bodyA.Materiel.restitution, bodyB.Materiel.restitution)) * (relVitAB) * wNormal;

            double impulse = MassEff > Config.EpsilonsFloat ? j / MassEff : 0f;
            KVector2 momentum = impulse * wNormal;

            bodyA.AddImpulseAtRelPosToCenter(-momentum, rA);
            bodyB.AddImpulseAtRelPosToCenter(momentum, rB);    

        }
#endregion
#region PositionConstraint

        private static void AdjustPositions(Contact c)
        {
            Rigidbody bodyA = c.FixtureA.Body;
            Rigidbody bodyB = c.FixtureB.Body;

            KVector2 wNormal = c.Manifold.WNormal;

            double maxPene = c.Manifold.ContactPoints[0].WPenetration;
            KVector2 farestPoint = c.Manifold.ContactPoints[0].WPosition;
            for (int i = 1; i < c.Manifold.Count; i++)
            {
                double wPenetration = c.Manifold.ContactPoints[i].WPenetration;

                if (wPenetration > maxPene)
                {
                    maxPene = wPenetration;
                    farestPoint = c.Manifold.ContactPoints[i].WPosition;
                }
            }

            ApplyPositionalCorrection(bodyA, bodyB, wNormal, maxPene, farestPoint);
        }

        private static void ApplyPositionalCorrection(Rigidbody bodyA, Rigidbody bodyB, KVector2 wNormal, double wPenetration, KVector2 wPosition)
        {
            KVector2 rA = wPosition - bodyA.State.Transform.TransformPointLW(bodyA.MassData.CenterOfMass);
            KVector2 rB = wPosition - bodyB.State.Transform.TransformPointLW(bodyB.MassData.CenterOfMass);

            double rAXwNormal = wNormal * (bodyA.MassData.InvInertia * (rA % wNormal) % rA);
            double rBXwNormal = wNormal * (bodyB.MassData.InvInertia * (rB % wNormal) % rB);

            double MassEff = bodyA.MassData.InvMass + bodyB.MassData.InvMass ;

            double impulse = MassEff > 0f ? wPenetration/MassEff : 0f; 
            KVector2 linMomentum = impulse * wNormal;

            bodyA.AddImpulseAtRelPosToCenter(-linMomentum, rA);
            bodyB.AddImpulseAtRelPosToCenter(linMomentum, rB);
        }
    }
#endregion
}
