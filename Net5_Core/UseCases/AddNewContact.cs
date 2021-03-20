using Net5_Core.Interfaces;
using Net5_Core.Models;
using System;
using System.Threading.Tasks;

namespace Net5_Core.UseCases
{
    public class AddNewContact
    {
        readonly IEFUoW _eFUoW;

        public AddNewContact(IEFUoW eFUoW)
        {
            _eFUoW = eFUoW;
        }

        public async Task<string> Execute(Contact contact)
        {
            if (contact == null)
                throw new ClientError("Add contact operation failed. Invalid contact received.");

            if (contact.Id != 0)
                throw new ClientError("Add contact operation failed. Invalid contact received.");


            contact.Trim();

            contact.Validate();


            try
            {
                await _eFUoW.BeginTransactionAsync();

                _eFUoW.ContactsRepo.Insert(contact);

                await _eFUoW.SaveChnages();

                await _eFUoW.SaveTransactionAsync();

                return "Success";
            }
            catch
            {
                await _eFUoW.RollbackTransactionAsync();
                throw;
            }


        }
    }
}
