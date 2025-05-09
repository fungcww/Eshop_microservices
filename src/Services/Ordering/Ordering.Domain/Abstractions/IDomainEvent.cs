using MediatR;

namespace Ordering.Domain.Abstractions
{
    public class IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        DateTime OccurredOn => DateTime.UtcNow;
        string EventType => GetType().AssemblyQualifiedName;
    }
}
