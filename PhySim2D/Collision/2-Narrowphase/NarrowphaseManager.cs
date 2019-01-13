using System.Collections.Generic;

namespace PhySim2D.Collision.Narrowphase
{
    internal class NarrowphaseManager
    {
        /// <summary>
        /// Method who return a list of collider who are colliding
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        ///
        public List<Contact> SpotCollision(List<Contact> contacts)
        {
            List<Contact> contactInCollision = new List<Contact>();

            for(int i = 0; i < contacts.Count; i++)
            {
                Contact c = contacts[i];
                if (CollisionDetection.Collision(ref c))
                    contactInCollision.Add(c);
            }

            return contactInCollision;
        }
    }
}
