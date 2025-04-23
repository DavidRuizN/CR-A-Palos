using Common.Domain.DomainEvents;
using Common.Domain.Interfaces;

namespace Common.Domain.Entities;

public abstract class Entity<T> : IEquatable<Entity<T>>, IEntityWithEvents
{
    private int? _requestedHashCode;
    private T? _id;
    private readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> DomainEvents => _domainEvents;

    public virtual T? Id
    {
        get => _id;
        protected set => _id = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T>)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return GetType() == obj.GetType() && base.Equals(obj);
    }

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.IsTransient() || IsTransient())
            return false;

        return Id?.Equals(other.Id) ?? false;
    }

    public override int GetHashCode()
    {
        if (IsTransient()) return base.GetHashCode();
        _requestedHashCode ??= (Id?.GetHashCode() ?? 0) ^ 31;
        return _requestedHashCode.Value;
    }

    public static bool operator ==(Entity<T> left, Entity<T> right)
    {
        return left?.Equals(right) ?? Equals(right, null);
    }

    public static bool operator !=(Entity<T> left, Entity<T> right)
    {
        return !(left == right);
    }

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private bool IsTransient()
    {
        return Id?.Equals(default(T)) ?? false;
    }
}
