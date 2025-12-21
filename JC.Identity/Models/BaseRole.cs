using Microsoft.AspNetCore.Identity;

namespace JC.Identity.Models;

public class BaseRole : IdentityRole
{
    public string? Description { get; set; }
}