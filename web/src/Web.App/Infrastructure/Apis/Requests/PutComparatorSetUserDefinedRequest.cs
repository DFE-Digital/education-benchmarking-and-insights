namespace Web.App.Infrastructure.Apis;

public record PutComparatorSetUserDefinedRequest
{
    public string? URN { get; set; }
    public Guid Identifier { get; set; } = Guid.NewGuid();
}