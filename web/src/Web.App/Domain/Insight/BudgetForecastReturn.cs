namespace Web.App.Domain;

public record BudgetForecastReturn
{
    public int? Year { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? ForecastTotalPupils { get; set; }
    public decimal? ActualTotalPupils { get; set; }

    public decimal? Variance { get; set; }
    public decimal? PercentVariance { get; set; }
    public string? VarianceStatus { get; set; }
}

public record BudgetForecastReturnMetric
{
    public int? Year { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }

    public BudgetForecastReturnMetricType MetricType => new(Metric);
}

public class BudgetForecastReturnMetricType(string? metric)
{
    public const string ExpenditureAsPercentageOfIncome = "Expenditure as percentage of income";
    public const string GrantFundingAsPercentageOfIncome = "Grant funding as percentage of income";
    public const string RevenueReserveAsPercentageOfIncome = "Revenue reserve as percentage of income";
    public const string SelfGeneratedIncomeAsPercentageOfIncome = "Self generated income as percentage of income";
    public const string Slope = "Slope";
    public const string SlopeFlag = "Slope flag";
    public const string StaffCostsAsPercentageOfIncome = "Staff costs as percentage of income";

    public bool IsSlope() => string.Equals(metric, Slope);
    public bool IsSlopeFlag() => string.Equals(metric, SlopeFlag);
    public bool IsStaffCosts() => string.Equals(metric, StaffCostsAsPercentageOfIncome);
}

public class BudgetForecastVarianceStatusType
{
    public const string ArSignificantlyBelowForecast = "AR significantly below forecast";
    public const string ArBelowForecast = "AR below forecast";
    public const string StableForecast = "Stable forecast";
    public const string ArAboveForecast = "AR above forecast";
    public const string ArSignificantlyAboveForecast = "AR significantly above forecast";
}