using Net5_Core.Interfaces.Repos;
using Net5_Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Net5_Infrastructure.Data.Repos
{
    public class ContactsRepo : IContactsRepo
    {
        private readonly Net5DataContext _context;

        public ContactsRepo(Net5DataContext context)
        {
            _context = context;
        }

        public void Delete(List<Contact> contacts)
        {
            _context.Contacts.RemoveRange(contacts);
        }

        public void Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetBy(int contactId)
        {
            return _context.Contacts.FirstOrDefault(x => x.Id == contactId);
        }

        public void Insert(List<Contact> contacts)
        {
            _context.Contacts.AddRange(contacts);
        }

        public void Insert(Contact contact)
        {
            _context.Contacts.Add(contact);
        }
    }
}
