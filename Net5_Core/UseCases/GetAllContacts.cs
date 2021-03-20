using Net5_Core.Interfaces;
using Net5_Core.Models;
using System.Collections.Generic;

namespace Net5_Core.UseCases
{
    public class GetAllContacts
    {
        private readonly IEFUoW _eFUoW;

        public GetAllContacts(IEFUoW eFUoW)
        {
            _eFUoW = eFUoW;
        }

        public List<Contact> Execute()
        {
            return _eFUoW.ContactsRepo.GetAll();
        }
    }
}
