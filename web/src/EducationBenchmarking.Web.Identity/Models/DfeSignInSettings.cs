using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeSignInSettings
{
    public string ClientID { get; set; }
    public string ClientSecret { get; set; }
    public string TokenEndPoint { get; set; }
    public string MetadataAddress { get; set; }
    public string CallbackPath { get; set; }
    public string SignedOutCallbackPath { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string APISecret { get; set; }
    public string APIUri { get; set; }
    public string SignOutUri { get; set; }
}