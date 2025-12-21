using Microsoft.EntityFrameworkCore.Storage;

namespace JC.Core.Services.DataRepositories;

public interface IRepositoryManager
{
    IRepositoryContext<T> GetRepository<T>() where T : class;

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
