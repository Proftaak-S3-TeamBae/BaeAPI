using BaeOpenAiIntegration.Service;
using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers;

/// <summary>
/// The controller for handling the OpenAI integration.
/// </summary>
[ApiController]
[Route("api/integration/[controller]")]
public class OpenAiController : Controller
{
    private readonly IOpenAiService _openAiService;

    public OpenAiController(IOpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    /// <summary>
    /// Register an OpenAI API key.
    /// This will get stored in the database.
    /// </summary>
    /// <returns></returns>
    [HttpPost("register-key")]
    public async Task<IActionResult> RegisterKey(string key)
    {
        await _openAiService.RegisterKeyAsync(key);
        return Ok();
    }

    /// <summary>
    /// Remove an OpenAI API key.
    /// This will remove the key from the database.
    /// </summary>
    /// <returns></returns>
    [HttpDelete("remove-key")]
    public async Task<IActionResult> RemoveKey()
    {
        await _openAiService.RemoveKeyAsync();
        return Ok();
    }
}
