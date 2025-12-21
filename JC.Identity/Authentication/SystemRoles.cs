using System.Reflection;

namespace JC.Identity.Authentication;

public class SystemRoles
{
    public const string SystemAdmin = nameof(SystemAdmin);
    public const string SystemAdminDesc = "Full system administrator with access to tenant management and assignment.";

    public const string Admin = nameof(Admin);
    public const string AdminDesc = "Administrator with access to all features within their tenant.";

    /// <summary>
    /// Gets all roles and their descriptions from this class and any derived class.
    /// Roles are paired with descriptions by convention: {RoleName} + {RoleName}Desc
    /// </summary>
    public static List<(string Role, string Description)> GetAllRoles<T>() where T : SystemRoles
    {
        var fields = typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Where(f => !f.Name.EndsWith("Desc"))
            .ToList();

        var result = new List<(string Role, string Description)>();

        foreach (var field in fields)
        {
            var role = (string?)field.GetRawConstantValue() ?? field.Name;
            var descField = typeof(T).GetField(
                $"{field.Name}Desc",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            var description = (string?)descField?.GetRawConstantValue() ?? string.Empty;
            result.Add((role, description));
        }

        return result;
    }
}