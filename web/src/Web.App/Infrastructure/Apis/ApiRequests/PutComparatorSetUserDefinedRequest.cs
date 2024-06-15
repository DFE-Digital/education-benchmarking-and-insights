namespace Web.App.Infrastructure.Apis;

public record PutComparatorSetUserDefinedRequest
{
    public Guid Identifier { get; set; } = Guid.NewGuid();
    public string[] Set { get; set; } = [];
    public string? UserId { get; set; }
}