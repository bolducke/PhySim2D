using PhySim2D.Sim;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Integrator
{
    class RK2 : IRigidbodyIntegrator
    {
        public void Integrate(Rigidbody body, float h)
        {

            State state = body.State;
            Derivative k1, k2;

            Derivative init = new Derivative
            {
                dPosition = body.State.Velocity,
                dRotation = body.State.AngVelocity,
                dVelocity = KVector2.Zero,
                dAngVelocity = 0f
            };
              
            k1 = RK.FindK(body, 0, init);
            k2 = RK.FindK(body, h * 0.5f, k1 * 0.5f);
            
            state.Transform.Position += (k2.dPosition + k1.dPosition) * 0.5f* h ;
            state.Transform.Rotation += (k2.dRotation + k1.dRotation) * 0.5f* h;
            state.Velocity += (k2.dVelocity + k1.dVelocity) * 0.5f * h;
            state.AngVelocity += (k2.dAngVelocity + k1.dAngVelocity) * 0.5f * h;

            state.ClearAccumulator();

            //Optimisation: Compute after angular and translation calculation
            state.Transform.SyncMatrix();
        }
    }
}
