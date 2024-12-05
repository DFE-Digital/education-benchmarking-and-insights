using System;
using System.Linq;
namespace Platform.Api.Insight.Domain;

public static class FinanceType
{
    private const string Academy = "Academy";
    private const string Maintained = "Maintained";

    public static readonly string[] All =
    [
        Academy,
        Maintained
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}