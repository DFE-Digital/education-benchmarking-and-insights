using System.Diagnostics.CodeAnalysis;

namespace Web.App.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeSignInOptions
{
    public bool IsDevelopment { get; set; }

    public DfeSignInEvents Events { get; } = new();

    public DfeSignInSettings Settings { get; } = new();
}