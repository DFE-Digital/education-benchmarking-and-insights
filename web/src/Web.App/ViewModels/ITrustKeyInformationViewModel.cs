namespace Web.App.ViewModels;

public interface ITrustKeyInformationViewModel
{
    decimal? InYearBalance { get; }
    decimal? RevenueReserve { get; }
    int NumberSchools { get; }
}