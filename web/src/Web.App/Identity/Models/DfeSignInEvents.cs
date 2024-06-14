using System.Diagnostics.CodeAnalysis;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
namespace Web.App.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeSignInEvents
{
    public Action<TokenResponse> OnRejectPrincipal { get; set; } = response => {};

    public Action<MessageReceivedContext> OnSpuriousAuthenticationRequest { get; set; } = context => {};

    public Action<RemoteFailureContext> OnRemoteFailure { get; set; } = ctx => {};

    public Action<TokenValidatedContext> OnValidatedPrincipal { get; set; } = ctx => {};

    public Action<TokenValidatedContext, Exception> OnNotValidatedPrincipal { get; set; } = (ctx, ex) => {};
}