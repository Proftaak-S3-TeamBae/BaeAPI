using BaeAuthentication;
using BaeServer.DTO.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers.Account;

/// <summary>
/// The controller for managing an account
/// </summary>
[ApiController]
[Route("/api/identity/account/[controller]")]
public class AccountController : Controller
{
    private IBaeAuthenticationService _authenticationService;

    public AccountController(IBaeAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Register an account
    /// </summary>
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<string>> OnPost([FromBody] RegisterDTO dto, [FromServices] UserManager<IdentityUser> userManager)
    {
        try
        {
            var token = await _authenticationService.RegisterUserAsync(userManager, dto.Username, dto.Email, dto.Password);
            return token;
        }
        catch (Exception e)
        {
            return Conflict(e);
        }
    }

    /// <summary>
    /// Login to an account
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<string>> OnPost([FromBody] LoginDTO dto, [FromServices] UserManager<IdentityUser> userManager)
    {
        try
        {
            var token = await _authenticationService.SignInUserAsync(userManager, dto.UsernameOrEmail, dto.Password);
            return token;
        }
        catch (Exception e)
        {
            return Conflict(e);
        }
    }
}
