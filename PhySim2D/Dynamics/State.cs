using PhySim2D.Dynamics.Forces;
using PhySim2D.Tools;
using System.Collections.Generic;

namespace PhySim2D.Dynamics.Integrator
{
    internal class State
    {

        public Dictionary<string, IForceGenerator> ForceGenerators { get; set; }

        public KTransform Transform { get; internal set; }

        //Linear
        public KVector2 Velocity { get; internal set; }
        public KVector2 ForcesAccumulate { get; internal set; }

        //Angular
        public double AngVelocity { get; internal set; }
        public double TorquesAccumulate { get; internal set; }

        public State()
        {
            Transform = new KTransform();
            Velocity = KVector2.Zero;
            ForcesAccumulate = KVector2.Zero;

            AngVelocity = 0f;
            TorquesAccumulate = 0f;

            ForceGenerators = new Dictionary<string, IForceGenerator>();
        }

        public void ClearAccumulator()
        {
            TorquesAccumulate = 0;
            ForcesAccumulate = KVector2.Zero;
        }
    }
}
