using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Integrator
{
    internal struct Derivative
    {
        public KVector2 dPosition;
        public KVector2 dVelocity;

        public double dRotation;
        public double dAngVelocity;

        public static Derivative Multiply(Derivative d, float k)
        {
            Derivative output;
            output.dPosition = d.dPosition * k;
            output.dVelocity = d.dVelocity * k;
            output.dRotation = d.dRotation * k;
            output.dAngVelocity = d.dAngVelocity * k;

            return output;
        }

        public static Derivative operator *(Derivative d, float k)
        {
            return Multiply(d, k);
        }

        public static Derivative operator *(float k, Derivative d)
        {
            return Multiply(d, k);
        }
    }

    static class RK
    {

        public static Derivative FindK(Rigidbody body, float h, Derivative d)
        {
            State state = new State();
            state.Transform.Position = body.State.Transform.Position + d.dPosition * h;
            state.Transform.Rotation = body.State.Transform.Rotation + d.dRotation * h;
            state.Velocity = d.dPosition + d.dVelocity * h;
            state.AngVelocity = d.dRotation + d.dAngVelocity * h;

            Derivative output;
            output.dPosition = state.Velocity;
            output.dRotation = state.AngVelocity;

            body.Acceleration(state, h, out output.dVelocity, out output.dAngVelocity);

            return output;
        }

    }
}
