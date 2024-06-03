namespace Web.App.Infrastructure.Apis;

public record PutComparatorSetUserDefinedRequest
{
    public string? URN { get; set; }
    public Guid Identifier { get; set; } = Guid.NewGuid();
    public string[] Set { get; set; } = Array.Empty<string>();
    public string? UserId { get; set; }
}