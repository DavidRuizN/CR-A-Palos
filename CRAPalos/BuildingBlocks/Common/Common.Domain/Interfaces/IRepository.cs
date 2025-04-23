namespace Common.Domain.Interfaces;

public interface IRepository<TModel> : IDisposable where TModel : class
{
    IUnitOfWork UnitOfWork { get; }

    Task<TModel> GetByIdAsync(params object[] id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<TModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    TModel Add(TModel model)
    {
        throw new NotImplementedException();
    }

    TModel Delete(TModel model)
    {
        throw new NotImplementedException();
    }

    TModel Update(TModel model)
    {
        throw new NotImplementedException();
    }

    void IDisposable.Dispose()
    {
    }
}
