using PhySim2D.Collision.Colliders;

namespace PhySim2D.Dynamics
{
    internal struct Fixture
    {
        public Rigidbody Body;

        public Collider Collider;

        public Fixture(Rigidbody body, Collider collider)
        {
            this.Body = body;
            Collider = collider;
        }

        public static void Swap(ref Fixture a, ref Fixture b)
        {
            Fixture temp = a;
            a = b;
            b = temp;
        }

    }
}
