namespace JC.Identity.Models.MultiTenancy;

public interface IMultiTenancy
{
    string? TenantId { get; set; }
    Tenant? Tenant { get; set; }
}