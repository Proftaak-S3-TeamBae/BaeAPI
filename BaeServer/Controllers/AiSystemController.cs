using BaeAiSystem;
using BaeServer.DTO.AiSystem;
using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers;

/// <summary>
/// The controller for fetching detected AI systems from the integrations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AiSystemController : Controller
{
    private readonly IAiSystemService _aiSystemService;

    public AiSystemController(IAiSystemService aiSystemService)
    {
        _aiSystemService = aiSystemService;
    }

    /// <summary>
    /// Get all detected AI systems from the integrations related to the user.
    /// </summary>
    /// <returns>The detected AI systems from the integrations.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AiSystemDTO>>> Get()
    {
        var aiSystem = await _aiSystemService.GetAiSystemsAsync();
        return Ok(aiSystem.Select(aiSystem => new AiSystemDTO
        {
            Name = aiSystem.Name,
            Type = aiSystem.Type,
            Source = aiSystem.Source,
            Description = aiSystem.Description,
            DateAdded = aiSystem.DateAdded
        }));
    }
}