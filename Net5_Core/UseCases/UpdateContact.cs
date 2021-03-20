using Net5_Core.Interfaces;
using Net5_Core.Models;
using System;
using System.Threading.Tasks;

namespace Net5_Core.UseCases
{
    public class UpdateContact
    {
        private readonly IEFUoW _eFUoW;

        public UpdateContact(IEFUoW eFUoW)
        {
            _eFUoW = eFUoW;
        }

        public async Task<string> Execute(Contact contact)
        {
            if (contact == null)
                throw new ClientError("Failed to update contact. Invalid contact details recieved.");


            try
            {
                await _eFUoW.BeginTransactionAsync();

                var targetContact = _eFUoW.ContactsRepo.GetBy(contact.Id);

                if (targetContact == null)
                    throw new ClientError("Failed to update contact. User not found.");

                targetContact.Update(contact);

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
