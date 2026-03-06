namespace BeaconFlow.Api.Contracts;
public sealed record IngestEventRequest(
    string Message,
    string Severity
);
