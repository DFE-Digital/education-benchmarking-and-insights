namespace Web.App.Infrastructure.Apis;

public record PostComparatorSetUserDefinedRequest
{
    public Guid? Identifier { get; set; } = Guid.NewGuid(); // todo: remove once Trust complete
    public string[] Set { get; set; } = [];
    public string? UserId { get; set; }
}