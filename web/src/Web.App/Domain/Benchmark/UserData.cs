namespace Web.App.Domain.Benchmark;

public record UserData
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}