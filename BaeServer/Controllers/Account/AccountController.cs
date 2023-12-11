using System.Security.Claims;
using BaeAuthentication;
using BaeServer.DTO.Account;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaeServer.Controllers.Account;

/// <summary>
/// The controller for managing an account
/// </summary>
[ApiController]
[Route("/api/identity/[controller]")]
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
    public async Task<ActionResult<string>> Register([FromBody] RegisterDTO dto, [FromServices] UserManager<IdentityUser> userManager)
    {
        var token = await _authenticationService.RegisterUserAsync(userManager, dto.Username, dto.Email, dto.Password);
        return token;
    }

    /// <summary>
    /// Login to an account
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO dto, [FromServices] UserManager<IdentityUser> userManager)
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

    /// <summary>
    /// Check if a token is valid and refresh it
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("check")]
    [Authorize]
    public ActionResult<string> CheckToken()
    {
        try
        {
            return _authenticationService.RefreshToken(User);
        }
        catch (Exception e)
        {
            return Conflict(e);
        }
    }

    /// <summary>
    /// Get the current username
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("username")]
    [Authorize]
    public ActionResult<string> GetUsername()
    {
        try
        {
            return User.FindFirstValue(JwtClaimTypes.Name) ?? throw new Exception("User not found");
        }
        catch (Exception e)
        {
            return Conflict(e);
        }
    }
}
