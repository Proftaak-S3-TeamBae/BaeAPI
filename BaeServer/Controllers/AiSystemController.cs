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
    /// <summary>
    /// Get all detected AI systems from the integrations related to the user.
    /// </summary>
    /// <returns>The detected AI systems from the integrations.</returns>
    [HttpGet]
    public ActionResult<List<AiSystemDTO>> Get()
    {
        return Ok(new List<AiSystemDTO> {
            new()
            {
                Name = "gpt-3.5",
                Type = "LLM",
                Source = "OpenAI",
                Description = "Chatbot that can be used to generate text.",
                DateAdded = DateTime.Now
            },
            new ()
            {
                Name = "gpt-4",
                Type = "LLM",
                Source = "OpenAI",
                Description = "Chatbot that can be used to generate text.",
                DateAdded = DateTime.Now
            }
        });
    }
}