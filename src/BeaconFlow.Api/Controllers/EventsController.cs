using BeaconFlow.Api.Contracts;
using BeaconFlow.Application.Abstractions;
using BeaconFlow.Application.Events.Ingest;
using BeaconFlow.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace BeaconFlow.Api.Controllers;

[ApiController]
[Route("api/events")]
public sealed class EventsController : ControllerBase
{
    private readonly IngestEventHandler _handler;
    private readonly IEventRepository _repo;

    public EventsController(IngestEventHandler handler, IEventRepository repo)
    {
        _handler = handler;
        _repo = repo;
    }

    [HttpPost]
    public async Task<ActionResult<object>> Ingest([FromBody] IngestEventRequest request, CancellationToken ct)
    {
        if (!Enum.TryParse<EventSeverity>(request.Severity, true, out var severity))
        {
            return BadRequest(new { error = "Invalid severity. Use: Info, Warning, Error, Critical." });
        }

        var id = await _handler.HandleAsync(
            new IngestEventCommand(request.Message, severity),
            ct);

        return Ok(new { id });
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<object>>> GetAll(CancellationToken ct)
    {
        var events = await _repo.GetAllAsync(ct);

        var response = events.Select(e => new
        {
            e.Id,
            e.Message,
            Severity = e.Severity.ToString(),
            e.Timestamp
        }).ToList();

        return Ok(response);
    }
}


