using PhySim2D.Dynamics.Integrator;
using PhySim2D.Tools;

namespace PhySim2D.Dynamics.Forces
{
    class GravityHMR : IForceGenerator
    {
        static readonly KVector2 defaultG = new KVector2(0, -9.8f);

        private KVector2 _gravity;

        private KVector2 _force;

        public GravityHMR() : this(defaultG) {}

        public GravityHMR(KVector2 g)
        {
            this._gravity = g;
        }

        public KVector2 GetForceApplied()
        {
            return _force;
        }

        public (KVector2, float) UpdateForce(MassData massData, PhysicMateriel materiel, State state, float h)
        {
            _force = _gravity * massData.Mass;
            return (_force, 0f);
        }
    }
}
