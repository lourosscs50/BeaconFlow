using BeaconFlow.Domain.Events;

namespace BeaconFlow.Application.Abstractions;
public interface IEventRepository
{
    Task AddAsync(EventRecord record, CancellationToken ct = default);
    Task<IReadOnlyList<EventRecord>> GetAllAsync(CancellationToken ct = default);
}