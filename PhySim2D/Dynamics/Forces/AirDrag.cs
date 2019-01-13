using PhySim2D.Dynamics.Integrator;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Forces
{
    class AirDrag : IForceGenerator
    {
        private float _b;
        private float _c;

        private KVector2 fDrag;

        public AirDrag(float b , float c)
        {
            this._b = b;
            this._c = c;
        }

        public KVector2 GetForceApplied()
        {
            return fDrag;
        }

        public (KVector2,float) UpdateForce(MassData massData, PhysicMateriel materiel, State state, float h)
        {
            fDrag = KVector2.Normalize(state.Velocity);

            double dragCoeff = state.Velocity.Length();
            dragCoeff = -_b * dragCoeff + _c * dragCoeff * dragCoeff;

            return (fDrag * dragCoeff,0f);
        }
    }
}
