using System.Collections.Generic;

namespace PhySim2D.Collision.Contacts
{
    interface IContactSolver
    {
        void SolveContact(List<Contact> contacts);

    }
}
