using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Constraint
{
    class DistanceConstraintSI : ConstraintSI
    {
        private float _distance;

        public DistanceConstraintSI(Rigidbody bodyA, Rigidbody bodyB, float distance) : base(bodyA, bodyB)
        {
            _distance = distance;
        }

        public override void SolveConstraints()
        {
            SolveSecondDerivativeConstraint();

            SolveFirstDerivativeConstraint();

        }

        public override void SolveFirstDerivativeConstraint()
        {
            KVector2 rA = KVector2.Zero;
            KVector2 rB = KVector2.Zero;
            KVector2 length = bodyA.State.Transform.TransformPointLW(bodyA.MassData.CenterOfMass) - bodyB.State.Transform.TransformPointLW(bodyB.MassData.CenterOfMass);
            KVector2 normal = -KVector2.Normalize(length);

            double rAXwNormal = (bodyA.MassData.InvInertia * (rA % normal) * (rA % normal));
            double rBXwNormal = (bodyB.MassData.InvInertia * (rB % normal) * (rB % normal));

            double C = length.Length() - _distance;
            double MassEff = bodyA.MassData.InvMass + bodyB.MassData.InvMass + rAXwNormal + rBXwNormal;

            double j = -MassEff * C;

            KVector2 impulse = j * normal;

            bodyA.AddImpulseAtRelPosToCenter(-impulse, rA);
            bodyB.AddImpulseAtRelPosToCenter(impulse, rB);
        }

        public override void SolveSecondDerivativeConstraint()
        {
            KVector2 rA = KVector2.Zero;
            KVector2 rB = KVector2.Zero;
            KVector2 normal = -KVector2.Normalize(bodyA.State.Transform.TransformPointLW(bodyA.MassData.CenterOfMass) - bodyB.State.Transform.TransformPointLW(bodyB.MassData.CenterOfMass));
            KVector2 relVitAB = bodyB.State.Velocity + bodyB.State.AngVelocity % rB - bodyA.State.Velocity - bodyA.State.AngVelocity % rA;

            double rAXwNormal = (bodyA.MassData.InvInertia * (rA % normal) * (rA % normal));
            double rBXwNormal = (bodyB.MassData.InvInertia * (rB % normal) * (rB % normal));

            double MassEff = bodyA.MassData.InvMass + bodyB.MassData.InvMass + rAXwNormal + rBXwNormal;

            double Jv = normal * relVitAB;

            double j = - MassEff * Jv;

            KVector2 impulse = j * normal;

            bodyA.AddImpulseAtRelPosToCenter(-impulse, rA);
            bodyB.AddImpulseAtRelPosToCenter(impulse, rB);

        }
    }
}
