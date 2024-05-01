using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public static class CostCategories
{
    public static Dictionary<int, string> Mapping => new()
    {
        { 1, "Teaching and teaching support staff" },
        { 2, "Non-educational support staff" },
        { 3, "Educational supplies" },
        { 4, "Educational ICT" },
        { 5, "Premises and services" },
        { 6, "Utilities" },
        { 7, "Administrative supplies" },
        { 8, "Catering staff and services" },
        { 9, "Other" }
    };
}