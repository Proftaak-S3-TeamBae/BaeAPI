using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers;

/// <summary>
/// The controller for handling the OpenAI integration.
/// </summary>
[ApiController]
[Route("api/integration/[controller]")]
public class OpenAiController : Controller
{
    /// <summary>
    /// Register an OpenAI API key.
    /// This will get stored in the database.
    /// </summary>
    /// <returns></returns>
    [HttpPost("register-key")]
    public Task<IActionResult> RegisterKey()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Remove an OpenAI API key.
    /// This will remove the key from the database.
    /// </summary>
    /// <returns></returns>
    [HttpDelete("remove-key")]
    public Task<IActionResult> RemoveKey()
    {
        throw new NotImplementedException();
    }
}
