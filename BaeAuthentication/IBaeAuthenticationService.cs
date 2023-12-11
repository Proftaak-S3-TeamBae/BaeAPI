using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BaeAuthentication;

/// <summary>
/// The authentication service 
/// </summary>
public interface IBaeAuthenticationService
{
    /// <summary>
    /// Register a user and return the jwt token
    /// </summary>
    /// <returns>The jwt token</returns>

    public Task<string> RegisterUserAsync(UserManager<IdentityUser> userManager, string username, string email, string password);

    /// <summary>
    /// Sign in to an account and return the jwt token
    /// </summary>
    /// <returns>The jwt token</returns>
    public Task<string> SignInUserAsync(UserManager<IdentityUser> userManager, string usernameOrEmail, string password);

    /// <summary>
    /// Verify a jwt token
    /// </summary>
    /// <returns>Whether jwt token is valid</returns>
    public Task<bool> VerifyTokenAsync(string token);

    /// <summary>
    /// Refresh a jwt token
    /// </summary>
    /// <returns>The new jwt token</returns>
    public string RefreshToken(ClaimsPrincipal claimsPrincipal);
}

