namespace Web.App.Infrastructure.Apis;

public record PostComparatorSetUserDefinedRequest
{
    public string[] Set { get; set; } = [];
    public string? UserId { get; set; }
}