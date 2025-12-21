namespace JC.Identity.Extensions.Options;

public class IdentityMiddlewareOptions
{
    public bool RequirePasswordChange { get; set; } = true;
    public string ChangePasswordRoute { get; set; } = "/Identity/Account/Manage/SetPassword";
    
    public bool EnforceTwoFactor { get; set; } = false;
    public string TwoFactorRoute { get; set; } = "/Identity/Account/Manage/EnableAuthenticator";
    
    public string AccessDeniedRoute { get; set; } = "/Identity/Account/AccessDenied";
    public string LogoutRoute { get; set; } = "/Identity/Account/Logout";
    public string ErrorRoute { get; set; } = "/Error";

    public string[] ExcludedPaths => [AccessDeniedRoute, LogoutRoute, ErrorRoute];
}