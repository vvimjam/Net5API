using Net5_Core.Interfaces.Repos;
using System;
using System.Threading.Tasks;

namespace Net5_Core.Interfaces
{
    public interface IEFUoW : IDisposable
    {
        public IContactsRepo ContactsRepo { get; }

        Task BeginTransactionAsync();
        
        Task SaveTransactionAsync();

        Task RollbackTransactionAsync();

        Task SaveChnages();
    }
}
