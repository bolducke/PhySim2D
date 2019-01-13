using PhySim2D.Collision.Colliders;
using PhySim2D.Tools;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhySim2D.Dynamics
{
    //TODO: Je devrais integrer la densite
    [DataContract]
    internal class MassData
    {
        private double _Mass;
        private double _Inertia;

        [DataMember]
        public double Mass
        {
            get
            {
                return _Mass;
            }
            private set
            {
                _Mass = value;
                if (value == 0)
                    InvMass = 0;
                else
                    InvMass = 1 / value;
            }
        }
        [DataMember]
        public double InvMass { get; private set; }


        [DataMember]
        public double Inertia
        {
            get
            {
                return _Inertia;
            }
            set
            {
                _Inertia = value;
                if (value == 0)
                    InvInertia = 0;
                else
                    InvInertia = 1 / value;
            }
        }

        [DataMember]
        public double InvInertia { get; private set; }

        [DataMember]
        public KVector2 CenterOfMass { get; set; }

        public MassData(double mass, double inertia) : this(mass, inertia, KVector2.Zero) { }

        public MassData(double mass, double inertia, KVector2 centerOfMass)
        {
            this.Mass = mass;
            this.Inertia = inertia;
            this.CenterOfMass = centerOfMass;
        }

        public void ComputeCenterOfMass(List<Collider> shapes)
        {
            KVector2 s = new KVector2();
            for(int i = 0; i < shapes.Count; i++)
            {
                s += shapes[i].Centroid;
            }

            CenterOfMass = s * (1 / shapes.Count);
        }

    }
}
