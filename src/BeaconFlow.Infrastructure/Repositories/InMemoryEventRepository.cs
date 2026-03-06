using BeaconFlow.Application.Abstractions;
using BeaconFlow.Domain.Events;

namespace BeaconFlow.Infrastructure.Repositories;

public sealed class InMemoryEventRepository : IEventRepository
{
    private readonly List<EventRecord> _events = new();

    public Task AddAsync(EventRecord record, CancellationToken ct = default)
    {
        _events.Add(record);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<EventRecord>> GetAllAsync(CancellationToken ct = default)
    {
        IReadOnlyList<EventRecord> result = _events;
        return Task.FromResult(result);
    }
}