using BeaconFlow.Domain.Events;
using BeaconFlow.Application.Abstractions;

namespace BeaconFlow.Application.Events.Ingest;
public sealed class IngestEventHandler
{
    private readonly IEventRepository _repo;
    public IngestEventHandler(IEventRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> HandleAsync(IngestEventCommand cmd, CancellationToken ct = default)
    {
        // Domain enforces the real rules (empty message, etc)
        var record = new EventRecord(cmd.Message, cmd.Severity);
        await _repo.AddAsync(record, ct);
        return record.Id;
    }
}
