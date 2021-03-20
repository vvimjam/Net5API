using Net5_Core.Models;
using System.Collections.Generic;

namespace Net5_Core.Interfaces.Repos
{
    public interface IContactsRepo
    {
        public List<Contact> GetAll();
        public Contact GetBy(int contactId);
        public void Insert(Contact contact);
        public void Insert(List<Contact> contacts);
        public void Delete(Contact contacts);
        public void Delete(List<Contact> contacts);
    }
}
