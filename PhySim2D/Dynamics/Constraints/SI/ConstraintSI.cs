namespace PhySim2D.Dynamics.Constraint
{
    internal abstract class ConstraintSI
    {
        protected Rigidbody bodyA;
        protected Rigidbody bodyB;

        public ConstraintSI(Rigidbody bodyA, Rigidbody bodyB)
        {
            this.bodyA = bodyA;
            this.bodyB = bodyB;
        }

        public abstract void SolveSecondDerivativeConstraint();

        public abstract void SolveFirstDerivativeConstraint();

        public abstract void SolveConstraints();
    }
}
