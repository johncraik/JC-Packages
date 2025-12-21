using JC.Core.Extensions;
using JC.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JC.Web.Helpers;

public static class DropdownHelper
{
    public static SelectListItem ToDropdownEntry(string text, string value, bool selected = false)
        => new()
        {
            Text = text,
            Value = value,
            Selected = selected
        };

    public static List<SelectListItem> FromEnum<T>(T? selected = null)
        where T : struct, Enum
        => Enum.GetValues<T>()
            .Select(e => ToDropdownEntry(
                e.ToDisplayName(),
                Convert.ToInt32(e).ToString(),
                selected.HasValue && EqualityComparer<T>.Default.Equals(e, selected.Value)))
            .ToList();

    public static List<SelectListItem> FromCollection<T>(
        IEnumerable<T> items,
        Func<T, string> textSelector,
        Func<T, string> valueSelector,
        Func<T, bool>? selectedPredicate = null)
        => items
            .Select(item => ToDropdownEntry(
                textSelector(item),
                valueSelector(item),
                selectedPredicate?.Invoke(item) ?? false))
            .ToList();

    public static List<SelectListItem> FromDictionary(
        Dictionary<string, string> items,
        string? selected = null)
        => items
            .Select(kvp => ToDropdownEntry(kvp.Value, kvp.Key, kvp.Key == selected))
            .ToList();

    public static List<SelectListItem> GetCountryDropdown(string? selected = null)
        => CountryHelper.GetCountries()
            .Select(c => ToDropdownEntry(c.Name, c.Code, string.Equals(c.Code, selected, StringComparison.OrdinalIgnoreCase)))
            .ToList();

    public static List<SelectListItem> WithPlaceholder(
        this List<SelectListItem> items,
        string text = "Please select...",
        string value = "")
    {
        items.Insert(0, new SelectListItem { Text = text, Value = value });
        return items;
    }
}