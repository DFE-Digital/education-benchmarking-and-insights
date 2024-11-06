namespace Web.App.ViewModels;

public interface ISchoolKeyInformationViewModel
{
    public decimal? InYearBalance { get; }
    public decimal? RevenueReserve { get; }
    public string? OfstedRating { get; }
    public string? OverallPhase { get; }
}