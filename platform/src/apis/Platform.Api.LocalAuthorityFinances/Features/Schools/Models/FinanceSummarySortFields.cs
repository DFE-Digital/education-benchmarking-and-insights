using System;
using System.Linq;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Models;

public static class FinanceSummarySortFields
{
    public const string SchoolName = nameof(FinanceSummaryResponse.SchoolName);
    public const string TotalPupils = nameof(FinanceSummaryResponse.TotalPupils);
    public const string TotalExpenditure = nameof(FinanceSummaryResponse.TotalExpenditure);
    public const string TotalTeachingSupportStaffCosts = nameof(FinanceSummaryResponse.TotalTeachingSupportStaffCosts);
    public const string RevenueReserve = nameof(FinanceSummaryResponse.RevenueReserve);

    public static readonly string[] All =
    [
        SchoolName,
        TotalPupils,
        TotalExpenditure,
        TotalTeachingSupportStaffCosts,
        RevenueReserve
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}

public static class SortDirection
{
    public const string Asc = "ASC";
    public const string Desc = "DESC";

    public static readonly string[] All = [Asc, Desc];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}