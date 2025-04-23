using Common.Domain.DomainEvents;

namespace Common.Domain.Interfaces;

public interface IEntityWithEvents
{
    List<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent eventItem);

    void ClearDomainEvents();
}
