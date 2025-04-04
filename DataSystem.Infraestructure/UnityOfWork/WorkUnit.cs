using DataSystem.Domain.UnitOfWork;
using DataSystem.Infraestructure.Context;

namespace DataSystem.Infraestructure.UnityOfWork
{
    public class WorkUnit : IWorkUnit
    {
        private readonly AppDbContext _appDbContext;
        public WorkUnit(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._appDbContext.Database.BeginTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await this._appDbContext.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            if (_appDbContext.Database.CurrentTransaction != null)
            {
                await _appDbContext.Database.CommitTransactionAsync();
            }
        }

        public void Rollback()
        {
            if (_appDbContext.Database.CurrentTransaction != null)
            {
                _appDbContext.Database.RollbackTransaction();
            }
        }

        public async Task DeleteAsync()
        {
            await this._appDbContext.Database.EnsureDeletedAsync();
        }

        public void BeginNewTransaction()
        {
            if (_appDbContext.Database.CurrentTransaction != null)
            {
                _appDbContext.Database.CurrentTransaction.Dispose();
            }

            _appDbContext.Database.BeginTransaction();
        }
    }
}
