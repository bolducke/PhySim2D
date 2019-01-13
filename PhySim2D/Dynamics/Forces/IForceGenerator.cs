using PhySim2D.Dynamics.Integrator;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Forces
{
    //TODO: Redefinir les createurs de forces pour inclure l'interaction entre deux particules.
    internal interface IForceGenerator
    {
        (KVector2,float) UpdateForce(MassData massData, PhysicMateriel materiel, State state, float h);

        KVector2 GetForceApplied();
    }
}
