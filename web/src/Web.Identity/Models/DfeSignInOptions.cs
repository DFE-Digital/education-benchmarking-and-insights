using System.Diagnostics.CodeAnalysis;

namespace Web.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeSignInOptions
{
    public bool IsDevelopment { get; set; }

    public DfeSignInEvents Events { get; } = new();

    public DfeSignInSettings Settings { get; } = new();
}