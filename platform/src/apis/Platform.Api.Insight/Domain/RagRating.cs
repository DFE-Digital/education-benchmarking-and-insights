using System;
using System.Linq;
namespace Platform.Api.Insight.Domain;

public static class RagRating
{
    internal const string Red = "red";
    private const string Amber = "amber";
    private const string Green = "green";

    public static readonly string[] All =
    [
        Red,
        Amber,
        Green
    ];

    public static bool IsValid(string? ragRating) => All.Any(a => a.Equals(ragRating, StringComparison.OrdinalIgnoreCase));
}