using BeaconFlow.Domain.Exceptions;

namespace BeaconFlow.Domain.Events;

// Core entity representing an event record in the system
public class EventRecord
{
    public Guid Id { get; private set; }
    public string Message { get; private set; }
    public EventSeverity Severity { get; private set; }
    public DateTime Timestamp { get; private set; }

    public EventRecord(string message, EventSeverity severity)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new DomainException("Event message cannot be empty.");

        Id = Guid.NewGuid();
        Message = message;
        Severity = severity;
        Timestamp = DateTime.UtcNow;
    }
}