using Web.App.Attributes;
namespace Web.App;

public enum TrackedEvents
{
    [StringValue("user-sign-in-initiated")]
    UserSignInInitiated,
    [StringValue("user-sign-in-success")]
    UserSignInSuccess
}