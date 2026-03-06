using BeaconFlow.Domain.Events;

namespace BeaconFlow.Application.Events.Ingest;
public sealed record IngestEventCommand(string Message, EventSeverity Severity);
