using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JC.Web.Helpers;

public class ModelStateWrapper(ModelStateDictionary modelState, string? prefix = null, bool ignorePrefix = false)
{
    private ModelStateDictionary ModelState => modelState;
    public bool IsValid => ModelState.IsValid;
    
    private readonly string _prefix = ignorePrefix 
        ? string.Empty 
        : string.IsNullOrEmpty(prefix) 
            ? "Input."
            : prefix.EndsWith('.') 
                ? prefix 
                : $"{prefix}.";
    
    public string this[string key] => 
        ModelState.TryGetValue($"{_prefix}{key}", out var entry) && entry.Errors.Count > 0
            ? entry.Errors[0].ErrorMessage
            : string.Empty;

    
    public void AddModelError(string key, string errorMessage) 
        => ModelState.AddModelError($"{_prefix}{key}", errorMessage);
    
    public bool HasError(string key)
        => ModelState.TryGetValue($"{_prefix}{key}", out var entry) && entry.Errors.Count > 0;

    public IEnumerable<string> GetErrors(string key)
        => ModelState.TryGetValue($"{_prefix}{key}", out var entry)
            ? entry.Errors.Select(e => e.ErrorMessage)
            : [];

    public Dictionary<string, string[]> GetAllErrors()
        => ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
}