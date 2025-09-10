namespace Web.App.ViewModels;

public interface ISchoolKeyInformationViewModel
{
    decimal? InYearBalance { get; }
    decimal? RevenueReserve { get; }
    string? OverallPhase { get; }
}