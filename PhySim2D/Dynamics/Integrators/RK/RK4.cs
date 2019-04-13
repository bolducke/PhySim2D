using PhySim2D.Sim;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Integrator
{
    internal class RK4 : IRigidbodyIntegrator
    {

        public void Integrate(Rigidbody body, float h)
        {

            State currentState = body.State;
            Derivative k1, k2, k3, k4;

            Derivative init = new Derivative
            {
                dPosition = body.State.Velocity,
                dRotation = body.State.AngVelocity,
                dVelocity = KVector2.Zero,
                dAngVelocity = 0f
            };

            k1 = RK.FindK(body, 0, init);
            k2 = RK.FindK(body, h * 0.5f, k1 *0.5f);
            k3 = RK.FindK(body, h * 0.5f, k2 *0.5f);
            k4 = RK.FindK(body, h, k3);

            const float mult = 1.0f/6.0f;

            currentState.Transform.Position += mult * (k1.dPosition + 2.0f*(k2.dPosition + k3.dPosition) + k4.dPosition) * h;
            currentState.Transform.Rotation += mult * (k1.dRotation + 2.0f * (k2.dRotation + k3.dRotation) + k4.dRotation) * h;
            currentState.Velocity += mult * (k1.dVelocity + 2.0f * (k2.dVelocity + k3.dVelocity) + k4.dVelocity) * h;
            currentState.AngVelocity += mult * (k1.dAngVelocity + 2.0f * (k2.dAngVelocity + k3.dAngVelocity) + k4.dAngVelocity) * h;

            currentState.ClearAccumulator();

            //Optimisation: Compute after angular and movement calculation
            currentState.Transform.SyncMatrix();
        }
    }
}
