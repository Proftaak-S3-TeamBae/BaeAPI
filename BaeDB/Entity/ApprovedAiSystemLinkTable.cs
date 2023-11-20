using System.Data.Common;

namespace BaeDB.Entity;

/// <summary>
/// The link table for approved AI systems.
/// </summary>
public class ApprovedAiSystemLinkTable
{
    public int Id { get; set; }
    public string AiSystemId { get; set; }
}
