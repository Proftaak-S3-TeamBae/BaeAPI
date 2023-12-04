using BaeAiSystem;
using BaeOpenAiIntegration.Service;
using BaeServer.API;
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
    private readonly IOpenAiService _openAiService;

    public AiSystemController(IAiSystemService aiSystemService, IOpenAiService openAiService)
    {
        _aiSystemService = aiSystemService;
        _openAiService = openAiService;
    }

    /// <summary>
    /// Get all detected AI systems from the integrations related to the user.
    /// </summary>
    /// <returns>The detected AI systems from the integrations.</returns>
    [HttpGet("scan")]
    public async Task<ActionResult<List<AiSystemDTO>>> Scan([FromQuery] int page = 0, [FromQuery] string? openaiKey = null)
    {
        if (openaiKey != null)
            _openAiService.RegisterKey(openaiKey);

        var systemList = await _aiSystemService.GetAiSystemsAsync();
        List<AiSystemDTO> systems = new();
        const int PAGE_MAX = 10;
        int startPos = page * PAGE_MAX;

        for (int i = startPos; i < startPos + PAGE_MAX; i++)
        {
            if (i >= systemList.Count)
                break;

            systems.Add(new AiSystemDTO
            {
                Name = systemList[i].Name,
                DateAdded = systemList[i].DateAdded,
                Description = systemList[i].Purpose,
                Version = systemList[i].Version,
                Integration = ((Integration)systemList[i].Integration).ToString(), // Hacky but not a crime!
                Type = systemList[i].Type
            });
        }

        // Remove the key from the registry.
        _openAiService.RemoveKey();

        return Ok(new PagedResponse<AiSystemDTO>
        {
            CurrentPage = page,
            TotalPages = systemList.Count / PAGE_MAX,
            Data = systems
        });
    }

    /// <summary>
    /// Approve an AI system.
    /// </summary>
    /// <param name="aiSystem">The AI systems to approve.</param>
    /// <returns></returns>
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] List<AiSystemDTO> aiSystems)
    {
        await _aiSystemService.ApproveAiSystemsAsync(aiSystems.Select(system => new AiSystem
        {
            Name = system.Name,
            DateAdded = system.DateAdded,
            Purpose = system.Description,
            Type = system.Type,
            Version = system.Version,
            Integration = (int)Enum.Parse(typeof(Integration), system.Integration)
        }).ToList());
        return Ok();
    }

    /// <summary>
    /// Disapprove an AI system.
    /// </summary>
    /// <param name="aiSystem">The AI systems to disapprove.</param>
    /// <returns></returns>
    [HttpPost("disapprove")]
    public async Task<IActionResult> Disapprove([FromBody] List<AiSystemDTO> aiSystems)
    {
        await _aiSystemService.DisapproveAiSystemsAsync(aiSystems.Select(system => new AiSystem
        {
            Name = system.Name,
            DateAdded = system.DateAdded,
            Purpose = system.Description,
            Type = system.Type,
            Version = system.Version,
            Integration = (int)Enum.Parse(typeof(Integration), system.Integration)
        }).ToList());
        return Ok();
    }

    /// <summary>
    /// Get all approved AI systems.
    /// </summary>
    /// <returns>The approved AI systems.</returns>
    [HttpGet("approved")]
    public async Task<ActionResult<List<AiSystemDTO>>> GetApproved()
    {
        var systemList = await _aiSystemService.GetApprovedAiSystemsAsync();
        List<AiSystemDTO> systems = new();

        foreach (var system in systemList)
        {
            systems.Add(new AiSystemDTO
            {
                Name = system.Name,
                DateAdded = system.DateAdded,
                Description = system.Purpose,
                Version = system.Version,
                Integration = ((Integration)system.Integration).ToString(), // Hacky but not a crime!
                Type = system.Type
            });
        }

        return Ok(systems);
    }

    /// <summary>
    /// Get all disapproved AI systems.
    /// </summary>
    /// <returns>The disapproved AI systems.</returns>
    [HttpGet("disapproved")]
    public async Task<ActionResult<List<AiSystemDTO>>> GetDisapproved()
    {
        var systemList = await _aiSystemService.GetDisapprovedAiSystemsAsync();
        List<AiSystemDTO> systems = new();

        foreach (var system in systemList)
        {
            systems.Add(new AiSystemDTO
            {
                Name = system.Name,
                DateAdded = system.DateAdded,
                Description = system.Purpose,
                Version = system.Version,
                Integration = ((Integration)system.Integration).ToString(), // Hacky but not a crime!
                Type = system.Type
            });
        }

        return Ok(systems);
    }

}