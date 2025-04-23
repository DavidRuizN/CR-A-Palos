using Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Common.Infrastructure.Database;

public class Repository<TModel> : IRepository<TModel> where TModel : class
{
    protected BaseContext Context { get; }
    protected DbSet<TModel> Set => Context.Set<TModel>();

    public IUnitOfWork UnitOfWork => Context;

    public Repository(IUnitOfWork context)
    {
        Context = (BaseContext)context;
    }

    public virtual TModel Add(TModel model)
    {
        return Set.Add(model).Entity;
    }

    public virtual TModel Delete(TModel model)
    {
        Set.Remove(model);

        return null;
    }

    public virtual async Task<TModel> GetByIDAsync(params object[] id)
    {
        var totalInLocal = Set.Local.Count;
        TModel model = await Set.FindAsync(id).ConfigureAwait(false);

        if (model != null)
        {
            var entry = Context.Entry(model);

            if (totalInLocal < Set.Local.Count)
            {
                foreach (var navigation in entry.Navigations)
                {
                    if (navigation.GetType() == typeof(ReferenceEntry) && MustLoadReference(navigation.Metadata.Name))
                    {
                        var reference = entry.Reference(navigation.Metadata.Name);
                        await reference.LoadAsync();
                    }
                    else if (navigation.GetType() == typeof(CollectionEntry) && MustLoadCollection(navigation.Metadata.Name))
                    {
                        var collection = entry.Collection(navigation.Metadata.Name);
                        await collection.LoadAsync();
                    }
                }
            }
        }

        return model;
    }

    protected virtual bool MustLoadCollection(string name)
    {
        return false;
    }

    protected virtual bool MustLoadReference(string name)
    {
        return true;
    }

    public virtual TModel Update(TModel model)
    {
        return Set.Update(model).Entity;
    }

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await Set.AsNoTracking().ToListAsync();
    }

    #endregion
}
