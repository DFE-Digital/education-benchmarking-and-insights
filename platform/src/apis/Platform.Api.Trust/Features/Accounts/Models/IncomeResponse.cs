﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public abstract record IncomeResponse
{
    public decimal? TotalIncome { get; set; }
    public decimal? TotalGrantFunding { get; set; }
    public decimal? TotalSelfGeneratedFunding { get; set; }
    public decimal? DirectRevenueFinancing { get; set; }
    public decimal? DirectGrants { get; set; }
    public decimal? PrePost16Funding { get; set; }
    public decimal? OtherDfeGrants { get; set; }
    public decimal? OtherIncomeGrants { get; set; }
    public decimal? GovernmentSource { get; set; }
    public decimal? CommunityGrants { get; set; }
    public decimal? Academies { get; set; }
    public decimal? IncomeFacilitiesServices { get; set; }
    public decimal? IncomeCatering { get; set; }
    public decimal? DonationsVoluntaryFunds { get; set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? InvestmentIncome { get; set; }
    public decimal? OtherSelfGeneratedIncome { get; set; }
}

[ExcludeFromCodeCoverage]
public record IncomeHistoryRowResponse : IncomeResponse
{
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record IncomeHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<IncomeHistoryRowResponse> Rows { get; set; } = [];
}