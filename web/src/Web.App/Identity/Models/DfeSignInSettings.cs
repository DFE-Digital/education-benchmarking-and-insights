using System.Diagnostics.CodeAnalysis;

namespace Web.App.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeSignInSettings
{
    public string? ClientID { get; set; }
    public string? ClientSecret { get; set; }
    public string? TokenEndPoint { get; set; }
    public string? MetadataAddress { get; set; }
    public string? CallbackPath { get; set; }
    public string? SignedOutCallbackPath { get; set; }
    public string? SignInUri { get; set; }
}