namespace Web.App.Domain.LocalAuthorities;

public record LocalAuthorityHeadlineStatistics
{
    public decimal? DsgHighNeedsAllocation { get; set; }
    public decimal? OutturnTotalHighNeeds { get; set; }
    public decimal? OutturnDsgCarriedForward { get; set; }
    public decimal? OutturnDsgCarriedForwardPreviousPeriod { get; set; }
}