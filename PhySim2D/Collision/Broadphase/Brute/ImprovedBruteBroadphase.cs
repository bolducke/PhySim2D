using System.Collections.Generic;
using PhySim2D.Collision.Colliders;
using PhySim2D.Dynamics;

namespace PhySim2D.Collision.Broadphase.Brute
{
    class ImprovedBruteBroadphase : BroadphaseManager
    {
        public override List<Contact> SpotPotentialCollision(List<Rigidbody> entities)
        {
            List<Contact> contacts = new List<Contact>();
             
            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i+1; j < entities.Count; j++)
                {
                        foreach (Collider c1 in entities[i].Colliders)
                        {
                            foreach (Collider c2 in entities[j].Colliders)
                            {
                                if(AABB.TryOverlap(c1.ComputeAABB(),c2.ComputeAABB()))
                                {
                                    Fixture fixtureA = new Fixture(entities[i], c1);

                                    Fixture fixtureB = new Fixture(entities[j], c2);

                                    Contact contact = new Contact(fixtureA, fixtureB);

                                    contacts.Add(contact);
                                }
                            }
                        }
                }
            }

            return contacts;
        }
    }
}
