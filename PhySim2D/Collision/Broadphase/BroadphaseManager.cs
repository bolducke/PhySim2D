using PhySim2D.Dynamics;
using System.Collections.Generic;

namespace PhySim2D.Collision.Broadphase
{
    /*
     *  Step 1:
     *  
     *  Spot pertinent collision
     *  
     *  Step 2: 
     *  
     *  Remove duplicate pair 
     * 
     * 
     */

    internal abstract class BroadphaseManager
    {
        /// <summary>
        /// Methods who return a list of pair of entities who are possibly colliding
        /// </summary>
        /// <param name="entities"> A list of entities presents in the simulation</param>
        /// <returns>A list of pair of entities possibly colliding</returns>
        public abstract List<Contact> SpotPotentialCollision(List<Rigidbody> entities);
    }
}
