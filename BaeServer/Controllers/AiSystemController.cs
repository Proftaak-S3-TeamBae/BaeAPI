using Microsoft.AspNetCore.Mvc;

namespace BaeServer.AiSystemController;

/// <summary>
/// The controller for fetching detected AI systems from the integrations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ApiSystemController : Controller
{
    /// <summary>
    /// Get all detected AI systems from the integrations related to the user.
    /// </summary>
    /// <returns>The detected AI systems from the integrations.</returns>
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new
        {
            Name = "Hello, world!"
        });
    }
}