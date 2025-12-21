using System.Reflection;

namespace JC.Core.Helpers;

public class ConstHelper
{
    public static Dictionary<string, object> GetAllConsts<T>()
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly) // const = literal, not initonly (readonly)
            .ToDictionary(f => f.Name, f => f.GetRawConstantValue()!);
    }
}