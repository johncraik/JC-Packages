namespace JC.Core.Models.Auditing;

public class AuditEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public AuditAction Action { get; set; }
    public DateTime AuditDate { get; set; }

    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? TableName { get; set; }
    public string? ActionData { get; set; }
}