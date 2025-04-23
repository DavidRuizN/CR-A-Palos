using Common.Domain.Interfaces;
using Common.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Infrastructure.Database;

public abstract class BaseContext : DbContext, IUnitOfWork
{
    private readonly IMediator? _mediator;
    private IDbContextTransaction? _currentTransaction;

    protected BaseContext(DbContextOptions options) : base(options)
    {
    }

    protected BaseContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public bool HasActiveTransaction => _currentTransaction != null;

    public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        if (_mediator != null)
        {
            await _mediator.DispatchDomainEventsAsync(this);
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result != 0;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _currentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            _currentTransaction?.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _currentTransaction?.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }
}
