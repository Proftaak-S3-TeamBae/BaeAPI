using BaeAiSystem;
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

    public AiSystemController(IAiSystemService aiSystemService)
    {
        _aiSystemService = aiSystemService;
    }

    /// <summary>
    /// Get all detected AI systems from the integrations related to the user.
    /// </summary>
    /// <returns>The detected AI systems from the integrations.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AiSystemDTO>>> Get([FromQuery] int page = 0)
    {
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
                Description = systemList[i].Description,
                Source = systemList[i].Source,
                Type = systemList[i].Type
            });
        }

        return Ok(new PagedResponse<AiSystemDTO>
        {
            CurrentPage = page,
            TotalPages = systemList.Count / PAGE_MAX,
            Data = systems
        });
    }
}