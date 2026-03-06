using BeaconFlow.Domain.Events;
using BeaconFlow.Domain.Exceptions;
using Xunit;

namespace BeaconFlow.Domain.Tests.Events;

public class EventRecordTests
{
    [Fact]
    public void CreateEvent_ShouldSetProperties()
    {
        var record = new EventRecord("System started", EventSeverity.Info);

        Assert.Equal("System started", record.Message);
        Assert.Equal(EventSeverity.Info, record.Severity);
        Assert.NotEqual(Guid.Empty, record.Id);
    }

    [Fact]
    public void CreateEvent_WithEmptyMessage_ShouldThrow()
    {
        Assert.Throws<DomainException>(() =>
            new EventRecord("", EventSeverity.Warning));
    }
}