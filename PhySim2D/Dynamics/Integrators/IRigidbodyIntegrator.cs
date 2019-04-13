using PhySim2D.Dynamics;

namespace PhySim2D.Sim
{
    internal interface IRigidbodyIntegrator
    {
        void Integrate(Rigidbody body, float h);
    }
}