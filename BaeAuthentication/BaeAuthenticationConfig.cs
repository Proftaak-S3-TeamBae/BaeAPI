namespace BaeAuthentication;

/// <summary>
/// The config for authentication
/// </summary>
public class BaeAuthenticationConfig
{
    public string EncryptionKey { get; set; }
    public int TokenLifetimeMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
