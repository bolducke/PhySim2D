using System.Runtime.Serialization;

namespace PhySim2D.Dynamics
{
    [DataContract]
    public struct PhysicMateriel
    {
        [DataMember]
        public float restitution;

        [DataMember]
        public float density;

        public PhysicMateriel(float rest, float density)
        {
            restitution = rest;
            this.density = density;
        }

    }
}
