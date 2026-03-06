using BeaconFlow.Application.Abstractions;
using BeaconFlow.Application.Events.Ingest;
using BeaconFlow.Domain.Events;
using BeaconFlow.Domain.Exceptions;
using Xunit;

namespace BeaconFlow.Application.Tests.Events.Ingest;

public sealed class IngestEventHandlerTests
{
    [Fact]
    public async Task HandleAsync_PersistsEvent_AndReturnsId()
    {
        var repo = new InMemoryEventRepository();
        var handler = new IngestEventHandler(repo);

        var id = await handler.HandleAsync(new IngestEventCommand(
            Message: "system started",
            Severity: EventSeverity.Info));

        Assert.NotEqual(Guid.Empty, id);
        Assert.Single(repo.Stored);
        Assert.Equal(id, repo.Stored[0].Id);
        Assert.Equal("system started", repo.Stored[0].Message);
        Assert.Equal(EventSeverity.Info, repo.Stored[0].Severity);
    }

    [Fact]
    public async Task HandleAsync_WithEmptyMessage_ThrowsDomainException()
    {
        var repo = new InMemoryEventRepository();
        var handler = new IngestEventHandler(repo);

        await Assert.ThrowsAsync<DomainException>(async () =>
            await handler.HandleAsync(new IngestEventCommand(
                Message: "",
                Severity: EventSeverity.Error)));
    }

    private sealed class InMemoryEventRepository : IEventRepository
    {
        public List<EventRecord> Stored { get; } = new();

        public Task AddAsync(EventRecord record, CancellationToken ct = default)
        {
            Stored.Add(record);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<EventRecord>> GetAllAsync(CancellationToken ct = default)
        {
            IReadOnlyList<EventRecord> results = Stored;
            return Task.FromResult(results);
        }
    }
}