using System.Data;

namespace TaskControl.Domain.Interfaces.Database
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        IDbConnection Connection { get; }
    }
}