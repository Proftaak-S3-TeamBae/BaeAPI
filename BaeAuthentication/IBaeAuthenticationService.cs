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
}

