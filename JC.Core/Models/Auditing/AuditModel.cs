namespace JC.Core.Models.Auditing;

public class AuditModel
{
    public string? CreatedById { get; private set; }
    public DateTime CreatedUtc { get; private set; }
    
    public string? LastModifiedById { get; private set; }
    public DateTime LastModifiedUtc { get; private set; }
    
    public string? DeletedById { get; private set; }
    public DateTime? DeletedUtc { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public string? RestoredById { get; private set; }
    public DateTime? RestoredUtc { get; private set; }
    
    public void FillCreated(string userId)
    {
        CreatedById = userId;
        CreatedUtc = DateTime.UtcNow;
    }
    
    public void FillModified(string userId)
    {
        LastModifiedById = userId;
        LastModifiedUtc = DateTime.UtcNow;
    }
    
    public void FillDeleted(string userId)
    {
        DeletedById = userId;
        DeletedUtc = DateTime.UtcNow;
        IsDeleted = true;
        
        RestoredById = null;
        RestoredUtc = null;
    }
    
    public void FillRestored(string userId)
    {
        RestoredById = userId;
        RestoredUtc = DateTime.UtcNow;
        IsDeleted = false;
        
        DeletedById = null;
        DeletedUtc = null;
    }
}