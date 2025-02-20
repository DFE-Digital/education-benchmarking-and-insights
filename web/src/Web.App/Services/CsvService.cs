using System.ComponentModel;

namespace Web.App.Services;

public interface ICsvService
{
    /// <summary>
    ///     Saves a collection of data objects to a CSV string.
    ///     <a href="https://stackoverflow.com/a/76324305/504477">ref</a>
    /// </summary>
    /// <param name="items">The collection of data objects.</param>
    /// <param name="exclude">The columns to exclude from the output file.</param>
    /// <returns>A string representation of the CSV file.</returns>
    string SaveToCsv(IEnumerable<object?> items, params string[] exclude);
}

public class CsvService : ICsvService
{
    public string SaveToCsv(IEnumerable<object?> items, params string[] exclude)
    {
        // get properties       
        var rows = items.Where(i => i != null).Cast<object>().ToArray();
        var objectType = rows.FirstOrDefault()?.GetType();
        if (objectType == null)
        {
            return string.Empty;
        }

        var props = TypeDescriptor.GetProperties(objectType).OfType<PropertyDescriptor>();
        var propertyNames = props
            .Select(p => p.Name)
            .Where(n => !exclude.Contains(n))
            .ToArray();
        var lines = new List<string>();

        // build header
        var header = string.Join(",", propertyNames);
        lines.Add(header);

        // build rows
        var valueLines = rows
            .Select(row =>
            {
                var values = propertyNames.Select(propertyName =>
                {
                    var propertyValue = row?.GetType().GetProperty(propertyName)?.GetValue(row, null);
                    var valueString = propertyValue?.ToString();

                    // quote values if they contain commas
                    return valueString?.Contains(',') == true ? $"\"{valueString}\"" : valueString;
                });

                return string.Join(",", values);
            });

        lines.AddRange(valueLines);
        return string.Join(Environment.NewLine, lines);
    }
}