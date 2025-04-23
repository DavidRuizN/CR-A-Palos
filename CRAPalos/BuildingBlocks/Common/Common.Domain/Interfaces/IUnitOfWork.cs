namespace Common.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); // Guarda

    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default); // Guarda y envia eventos de dominio
}
