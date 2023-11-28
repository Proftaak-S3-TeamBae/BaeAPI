using System.Text.Json.Serialization;

namespace BaeServer.DTO.Account;

/// <summary>
/// The dto for registering an account
/// </summary>
public struct RegisterDTO
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
