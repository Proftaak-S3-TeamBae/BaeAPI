using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BaeAuthentication;

/// <summary>
/// The authentication service 
/// </summary>
public class BaeAuthenticationService : IBaeAuthenticationService
{
    private readonly BaeAuthenticationConfig _config;

    public BaeAuthenticationService(BaeAuthenticationConfig config)
    {
        _config = config;
    }

    public async Task<string> RegisterUserAsync(UserManager<IdentityUser> userManager, string username, string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = username,
            Email = email
        };
        var result = await userManager.CreateAsync(user, password);

        /// Return error if failed to create user
        if (!result.Succeeded)
        {
            var errorString = "Failed to register user: \n";
            foreach (var error in result.Errors)
            {
                errorString += "    " + error + "\n";
            }
            throw new Exception(errorString);
        }

        // Generate JWT token
        var claimsIdentityUser = new ClaimsIdentity(new[]
        {
            new Claim(JwtClaimTypes.Name, user.UserName),
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.Id, user.Id)
        });
        return GenerateJwtToken(claimsIdentityUser);
    }

    public async Task<string> SignInUserAsync(UserManager<IdentityUser> userManager, string usernameOrEmail, string password)
    {
        var user = await userManager.FindByEmailAsync(usernameOrEmail);
        if (user == null)
            user = await userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            throw new Exception("User not found");


        var result = await userManager.CheckPasswordAsync(user, password);
        if (!result)
            throw new Exception("Incorrect password");

        var claimsIdentityUser = new ClaimsIdentity(new[]
        {
            new Claim(JwtClaimTypes.Name, user.UserName),
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.Id, user.Id)
        });
        return GenerateJwtToken(claimsIdentityUser);
    }

    /// <summary>
    /// Generate a jwt token
    /// </summary>
    /// <returns></returns>
    private string GenerateJwtToken(ClaimsIdentity user)
    {
        Console.WriteLine(_config.EncryptionKey.Length);
        var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.EncryptionKey));
        var encryptionCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = user,
            Expires = DateTime.UtcNow.AddMinutes(_config.TokenLifetimeMinutes),
            Issuer = _config.Issuer,
            Audience = _config.Audience,
            EncryptingCredentials = encryptionCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
