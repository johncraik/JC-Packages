using JC.Identity.Models.MultiTenancy;
using Microsoft.AspNetCore.Identity;

namespace JC.Identity.Models;

public class BaseUser : IdentityUser, IMultiTenancy
{
    public string? TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public string? DisplayName { get; set; }
    public DateTime? LastLoginUtc { get; set; }
    public bool IsEnabled { get; set; }
    
    public bool RequirePasswordChange { get; set; }
}