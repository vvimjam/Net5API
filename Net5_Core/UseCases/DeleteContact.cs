using Net5_Core.Interfaces;
using Net5_Core.Models;
using System;
using System.Threading.Tasks;

namespace Net5_Core.UseCases
{
    public class DeleteContact
    {
        private readonly IEFUoW _eFUoW;

        public DeleteContact(IEFUoW eFUoW)
        {
            _eFUoW = eFUoW;
        }

        public async Task<string> Execute(int contactId)
        {
            if (contactId <= 0)
                throw new ClientError("Failed to delete contact. Invalid contact id recieved.");



            var targetContact = _eFUoW.ContactsRepo.GetBy(contactId);

            if (targetContact == null)
                throw new ClientError("Failed to delete contact. User not found.");

            try
            {
                await _eFUoW.BeginTransactionAsync();

                _eFUoW.ContactsRepo.Delete(targetContact);

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
