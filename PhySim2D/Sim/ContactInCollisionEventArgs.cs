using PhySim2D.Collision;
using System.Collections.Generic;

namespace PhySim2D.Sim
{
    internal class ContactInCollisionEventArgs : System.EventArgs
    {
        public List<Contact> Contacts { get; set; }

        public ContactInCollisionEventArgs(List<Contact> contacts)
        {
            Contacts = contacts;
        }
    }
}