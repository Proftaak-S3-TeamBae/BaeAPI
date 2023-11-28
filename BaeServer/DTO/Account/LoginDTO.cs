using System.Text.Json.Serialization;

namespace BaeServer.DTO.Account;

/// <summary>
/// The login dto 
/// </summary>
[Serializable]
public struct LoginDTO
{
    [JsonPropertyName("usernameoremail")]
    public string UsernameOrEmail { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
