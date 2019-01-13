using PhySim2D.Dynamics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PhysicEngine.UnitTest")]
namespace PhySim2D.Collision
{
    internal struct Contact
    {
        public Fixture FixtureA;

        public Fixture FixtureB;

        public ContactManifold Manifold;

        public Contact(Fixture fixtureA, Fixture fixtureB)
        {
            FixtureA = fixtureA;
            FixtureB = fixtureB;
            Manifold = new ContactManifold();
        }

    }
}
