// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.Controllers.Api.Responses;

public record EducationHealthCarePlansHistoryResponse
{
    public int? Year { get; set; }
    public string? Term { get; set; }
    public decimal? Total { get; set; }
    public decimal? Mainstream { get; set; }
    public decimal? Resourced { get; set; }
    public decimal? Special { get; set; }
    public decimal? Independent { get; set; }
    public decimal? Hospital { get; set; }
    public decimal? Post16 { get; set; }
    public decimal? Other { get; set; }
}