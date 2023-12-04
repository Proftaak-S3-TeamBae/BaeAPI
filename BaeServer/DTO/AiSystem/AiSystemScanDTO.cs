namespace BaeServer.DTO.AiSystem;

/// <summary>
/// The DTO for an ai scan request
/// </summary>
public class AiSystemScanDTO
{
    /// <summary>
    /// The token for the OpenAI API integration
    /// </summary>
    public string? OpenAiToken { get; set; }
}
