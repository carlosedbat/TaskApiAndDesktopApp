namespace DataSystem.Domain.UnitOfWork
{
    using System.Threading.Tasks;
    public interface IWorkUnit
    {
        Task CommitAsync();

        Task DeleteAsync();

        void Rollback();

        Task SaveChangesAsync();

        void BeginNewTransaction();
    }
}
