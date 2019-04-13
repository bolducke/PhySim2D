using PhySim2D.Collision.Colliders;

namespace PhySim2D.Dynamics
{
    internal struct Fixture
    {
        public Rigidbody Body { get; set; }

        public Collider Collider { get; set; }

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
