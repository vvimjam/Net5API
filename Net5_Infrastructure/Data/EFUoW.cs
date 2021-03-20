using Microsoft.EntityFrameworkCore.Storage;
using Net5_Core.Interfaces;
using Net5_Core.Interfaces.Repos;
using Net5_Infrastructure.Data.Repos;
using System.Threading.Tasks;

namespace Net5_Infrastructure.Data
{
    public class EFUoW : IEFUoW
    {
        readonly Net5DataContext _context;
        IDbContextTransaction _dbTransaction;

        public EFUoW(Net5DataContext context)
        {
            _context = context;
        }

        IContactsRepo _contactsRepo;
        private bool disposedValue;

        public IContactsRepo ContactsRepo => _contactsRepo ??= new ContactsRepo(_context);

        public async Task SaveChnages()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveTransactionAsync()
        {
            await _dbTransaction.CommitAsync();
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbTransaction.RollbackAsync();
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }

        public async Task BeginTransactionAsync()
        {
            if (_dbTransaction == null)
                _dbTransaction = await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dbTransaction != null)
                    {
                        _dbTransaction.Rollback();
                        _dbTransaction.Dispose();
                        _dbTransaction = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
