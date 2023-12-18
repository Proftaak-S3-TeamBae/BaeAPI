using BaeAiSystem;
using BaeOpenAiIntegration.Service;
using BaeServer.API;
using BaeServer.DTO.AiSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers;

/// <summary>
/// The controller for fetching detected AI systems from the integrations.
/// </summary>
[Authorize]
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
    [HttpPost("scan")]
    public async Task<ActionResult<List<AiSystemDTO>>> Scan([FromBody] AiSystemScanDTO dto)
    {
        // Register openai keys
        if (dto.OpenAiTokens != null)
            foreach (var key in dto.OpenAiTokens)
                if (key != null)
                    _openAiService.RegisterKey(key);

        var systems = await _aiSystemService.GetAiSystemsAsync();
        var systemDtos = systems.Select(system => new AiSystemDTO
        {
            Name = system.Name,
            DateAdded = system.DateAdded,
            Description = system.Purpose,
            Version = system.Version,
            Integration = ((Integration)system.Integration).ToString(), // Hacky but not a crime!
            Origin = system.Origin,
            Type = system.Type
        }).ToList();

        _openAiService.RemoveKeys();
        return Ok(PagedResponse<AiSystemDTO>.FromList(systemDtos, dto.Page, dto.Max));
    }

    /// <summary>
    /// Add/Update an AI system.
    /// </summary>
    /// <param name="aiSystem">The AI system to add/update.</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] List<AiSystemDTO> aiSystem)
    {
        await _aiSystemService.ApproveAiSystemsAsync(aiSystem.Select(system => new AiSystem
        {
            Name = system.Name,
            DateAdded = system.DateAdded,
            Purpose = system.Description,
            Type = system.Type,
            Version = system.Version,
            Integration = (int)Enum.Parse(typeof(Integration), system.Integration),
            Origin = system.Origin,
        }).ToList());
        return Ok();
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
            Integration = (int)Enum.Parse(typeof(Integration), system.Integration),
            Origin = system.Origin,
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
            Integration = (int)Enum.Parse(typeof(Integration), system.Integration),
            Origin = system.Origin,
        }).ToList());
        return Ok();
    }

    /// <summary>
    /// Get all approved AI systems.
    /// </summary>
    /// <returns>The approved AI systems.</returns>
    [HttpGet("approved")]
    public async Task<ActionResult<List<AiSystemDTO>>> GetApproved([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
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
                Origin = system.Origin,
                Type = system.Type
            });
        }

        return Ok(PagedResponse<AiSystemDTO>.FromList(systems, page, pageSize));
    }

    /// <summary>
    /// Get all disapproved AI systems.
    /// </summary>
    /// <returns>The disapproved AI systems.</returns>
    [HttpGet("disapproved")]
    public async Task<ActionResult<List<AiSystemDTO>>> GetDisapproved([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
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
                Origin = system.Origin,
                Type = system.Type
            });
        }

        return Ok(PagedResponse<AiSystemDTO>.FromList(systems, page, pageSize));
    }


}