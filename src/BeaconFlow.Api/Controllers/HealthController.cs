using Microsoft.AspNetCore.Mvc;

namespace BeaconFlow.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<object> Get()
    {
        return Ok(new
        {
            status = "ok",
            service = "BeaconFlow",
            utc = DateTime.UtcNow
        });
    }
}