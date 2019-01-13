using PhySim2D.Sim;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Integrator
{
    internal class ExplicitEuler : IRigidbodyIntegrator

    {
        public void Integrate(Rigidbody body, float h)
        {
            //Setting Variable and calculating acceleration
            State currentState = body.State;
            body.Acceleration(currentState, h, out KVector2 acceleration, out double angularAcceleration);

            //Translation
            KVector2 velocity = currentState.Velocity + acceleration * h;
            KVector2 position = currentState.Transform.Position + currentState.Velocity * h;
            currentState.Velocity = velocity;
            currentState.Transform.Position = position;

            //Angular
            double angularVelocity = currentState.AngVelocity + angularAcceleration* h;
            double rotation = currentState.Transform.Rotation + currentState.AngVelocity * h;
            currentState.AngVelocity = angularVelocity;
            currentState.Transform.Rotation = rotation;

            currentState.ClearAccumulator();

            currentState.Transform.SyncMatrix();
        }
    }
}
