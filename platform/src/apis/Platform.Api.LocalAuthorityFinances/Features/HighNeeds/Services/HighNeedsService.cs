using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.Api.LocalAuthorityFinances.Shared;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

public interface IHighNeedsService
{
    Task<LocalAuthority<Models.HighNeeds>[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<History<HighNeedsYear>?> GetHistory(string[] codes, string dimension, CancellationToken cancellationToken = default);
}

public class HighNeedsService(IDatabaseFactory dbFactory) : IHighNeedsService
{
    public async Task<LocalAuthority<Models.HighNeeds>[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        string[] fields =
        [
            // LocalAuthorityBase
            "LaCode AS [Code]",
            "[Name]",
            // HighNeedsBase
            "OutturnTotalHighNeeds AS [Total]",
            // HighNeedsAmount
            "OutturnTotalPlaceFunding AS [TotalPlaceFunding]",
            "OutturnTotalTopUpFundingMaintained AS [TopUpFundingMaintained]",
            "OutturnTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained]",
            "OutturnTotalSenServices AS [SenServices]",
            "OutturnTotalAlternativeProvisionServices AS [AlternativeProvisionServices]",
            "OutturnTotalHospitalServices AS [HospitalServices]",
            "OutturnTotalOtherHealthServices AS [OtherHealthServices]",
            // TopFunding
            "OutturnTopFundingMaintainedEarlyYears AS [EarlyYears]",
            "OutturnTopFundingMaintainedPrimary AS [Primary]",
            "OutturnTopFundingMaintainedSecondary AS [Secondary]",
            "OutturnTopFundingMaintainedSpecial AS [Special]",
            "OutturnTopFundingMaintainedAlternativeProvision AS [AlternativeProvision]",
            "OutturnTopFundingMaintainedPostSchool AS [PostSchool]",
            "OutturnTopFundingMaintainedIncome AS [Income]",
            // TopFunding
            "OutturnTopFundingNonMaintainedEarlyYears AS [EarlyYears]",
            "OutturnTopFundingNonMaintainedPrimary AS [Primary]",
            "OutturnTopFundingNonMaintainedSecondary AS [Secondary]",
            "OutturnTopFundingNonMaintainedSpecial AS [Special]",
            "OutturnTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision]",
            "OutturnTopFundingNonMaintainedPostSchool AS [PostSchool]",
            "OutturnTopFundingNonMaintainedIncome AS [Income]",
            // PlaceFunding
            "OutturnPlaceFundingPrimary AS [Primary]",
            "OutturnPlaceFundingSecondary AS [Secondary]",
            "OutturnPlaceFundingSpecial AS [Special]",
            "OutturnPlaceFundingAlternativeProvision AS [AlternativeProvision]",
            // HighNeedsBase
            "BudgetTotalHighNeeds AS [Total]",
            // HighNeedsAmount
            "BudgetTotalPlaceFunding AS [TotalPlaceFunding]",
            "BudgetTotalTopUpFundingMaintained AS [TopUpFundingMaintained]",
            "BudgetTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained]",
            "BudgetTotalSenServices AS [SenServices]",
            "BudgetTotalAlternativeProvisionServices AS [AlternativeProvisionServices]",
            "BudgetTotalHospitalServices AS [HospitalServices]",
            "BudgetTotalOtherHealthServices AS [OtherHealthServices]",
            // TopFunding
            "BudgetTopFundingMaintainedEarlyYears AS [EarlyYears]",
            "BudgetTopFundingMaintainedPrimary AS [Primary]",
            "BudgetTopFundingMaintainedSecondary AS [Secondary]",
            "BudgetTopFundingMaintainedSpecial AS [Special]",
            "BudgetTopFundingMaintainedAlternativeProvision AS [AlternativeProvision]",
            "BudgetTopFundingMaintainedPostSchool AS [PostSchool]",
            "BudgetTopFundingMaintainedIncome AS [Income]",
            // TopFunding
            "BudgetTopFundingNonMaintainedEarlyYears AS [EarlyYears]",
            "BudgetTopFundingNonMaintainedPrimary AS [Primary]",
            "BudgetTopFundingNonMaintainedSecondary AS [Secondary]",
            "BudgetTopFundingNonMaintainedSpecial AS [Special]",
            "BudgetTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision]",
            "BudgetTopFundingNonMaintainedPostSchool AS [PostSchool]",
            "BudgetTopFundingNonMaintainedIncome AS [Income]",
            // PlaceFunding
            "BudgetPlaceFundingPrimary AS [Primary]",
            "BudgetPlaceFundingSecondary AS [Secondary]",
            "BudgetPlaceFundingSpecial AS [Special]",
            "BudgetPlaceFundingAlternativeProvision AS [AlternativeProvision]"
        ];

        Type[] types =
        [
            typeof(LocalAuthorityBase),
            typeof(HighNeedsBase),
            typeof(HighNeedsAmount),
            typeof(TopFunding),
            typeof(TopFunding),
            typeof(PlaceFunding),
            typeof(HighNeedsBase),
            typeof(HighNeedsAmount),
            typeof(TopFunding),
            typeof(TopFunding),
            typeof(PlaceFunding)
        ];

        string[] splitOn =
        [
            nameof(HighNeedsBase.Total),
            nameof(HighNeedsAmount.TotalPlaceFunding),
            nameof(TopFunding.EarlyYears),
            nameof(TopFunding.EarlyYears),
            nameof(PlaceFunding.Primary),
            nameof(HighNeedsBase.Total),
            nameof(HighNeedsAmount.TotalPlaceFunding),
            nameof(TopFunding.EarlyYears),
            nameof(TopFunding.EarlyYears),
            nameof(PlaceFunding.Primary)
        ];

        using var conn = await dbFactory.GetConnection();
        var laBuilder = new LocalAuthorityFinancialDefaultCurrentQuery(dimension, fields)
            .WhereLaCodesIn(codes);

        var results = await conn.QueryAsync(laBuilder, types, Mapper.MultiMapToHighNeeds, splitOn, cancellationToken);
        return results.ToArray();
    }

    public async Task<History<HighNeedsYear>?> GetHistory(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        string[] fields =
        [
            // HighNeedsYearBase
            "LaCode AS [Code]",
            "[RunId]",
            // HighNeedsBase
            "OutturnTotalHighNeeds AS [Total]",
            // HighNeedsAmount
            "OutturnTotalPlaceFunding AS [TotalPlaceFunding]",
            "OutturnTotalTopUpFundingMaintained AS [TopUpFundingMaintained]",
            "OutturnTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained]",
            "OutturnTotalSenServices AS [SenServices]",
            "OutturnTotalAlternativeProvisionServices AS [AlternativeProvisionServices]",
            "OutturnTotalHospitalServices AS [HospitalServices]",
            "OutturnTotalOtherHealthServices AS [OtherHealthServices]",
            // TopFunding
            "OutturnTopFundingMaintainedEarlyYears AS [EarlyYears]",
            "OutturnTopFundingMaintainedPrimary AS [Primary]",
            "OutturnTopFundingMaintainedSecondary AS [Secondary]",
            "OutturnTopFundingMaintainedSpecial AS [Special]",
            "OutturnTopFundingMaintainedAlternativeProvision AS [AlternativeProvision]",
            "OutturnTopFundingMaintainedPostSchool AS [PostSchool]",
            "OutturnTopFundingMaintainedIncome AS [Income]",
            // TopFunding
            "OutturnTopFundingNonMaintainedEarlyYears AS [EarlyYears]",
            "OutturnTopFundingNonMaintainedPrimary AS [Primary]",
            "OutturnTopFundingNonMaintainedSecondary AS [Secondary]",
            "OutturnTopFundingNonMaintainedSpecial AS [Special]",
            "OutturnTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision]",
            "OutturnTopFundingNonMaintainedPostSchool AS [PostSchool]",
            "OutturnTopFundingNonMaintainedIncome AS [Income]",
            // PlaceFunding
            "OutturnPlaceFundingPrimary AS [Primary]",
            "OutturnPlaceFundingSecondary AS [Secondary]",
            "OutturnPlaceFundingSpecial AS [Special]",
            "OutturnPlaceFundingAlternativeProvision AS [AlternativeProvision]",
            // HighNeedsBase
            "BudgetTotalHighNeeds AS [Total]",
            // HighNeedsAmount
            "BudgetTotalPlaceFunding AS [TotalPlaceFunding]",
            "BudgetTotalTopUpFundingMaintained AS [TopUpFundingMaintained]",
            "BudgetTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained]",
            "BudgetTotalSenServices AS [SenServices]",
            "BudgetTotalAlternativeProvisionServices AS [AlternativeProvisionServices]",
            "BudgetTotalHospitalServices AS [HospitalServices]",
            "BudgetTotalOtherHealthServices AS [OtherHealthServices]",
            // TopFunding
            "BudgetTopFundingMaintainedEarlyYears AS [EarlyYears]",
            "BudgetTopFundingMaintainedPrimary AS [Primary]",
            "BudgetTopFundingMaintainedSecondary AS [Secondary]",
            "BudgetTopFundingMaintainedSpecial AS [Special]",
            "BudgetTopFundingMaintainedAlternativeProvision AS [AlternativeProvision]",
            "BudgetTopFundingMaintainedPostSchool AS [PostSchool]",
            "BudgetTopFundingMaintainedIncome AS [Income]",
            // TopFunding
            "BudgetTopFundingNonMaintainedEarlyYears AS [EarlyYears]",
            "BudgetTopFundingNonMaintainedPrimary AS [Primary]",
            "BudgetTopFundingNonMaintainedSecondary AS [Secondary]",
            "BudgetTopFundingNonMaintainedSpecial AS [Special]",
            "BudgetTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision]",
            "BudgetTopFundingNonMaintainedPostSchool AS [PostSchool]",
            "BudgetTopFundingNonMaintainedIncome AS [Income]",
            // PlaceFunding
            "BudgetPlaceFundingPrimary AS [Primary]",
            "BudgetPlaceFundingSecondary AS [Secondary]",
            "BudgetPlaceFundingSpecial AS [Special]",
            "BudgetPlaceFundingAlternativeProvision AS [AlternativeProvision]"
        ];

        Type[] types =
        [
            typeof(HighNeedsYearBase),
            typeof(HighNeedsBase),
            typeof(HighNeedsAmount),
            typeof(TopFunding),
            typeof(TopFunding),
            typeof(PlaceFunding),
            typeof(HighNeedsBase),
            typeof(HighNeedsAmount),
            typeof(TopFunding),
            typeof(TopFunding),
            typeof(PlaceFunding)
        ];

        string[] splitOn =
        [
            nameof(HighNeedsBase.Total),
            nameof(HighNeedsAmount.TotalPlaceFunding),
            nameof(TopFunding.EarlyYears),
            nameof(TopFunding.EarlyYears),
            nameof(PlaceFunding.Primary),
            nameof(HighNeedsBase.Total),
            nameof(HighNeedsAmount.TotalPlaceFunding),
            nameof(TopFunding.EarlyYears),
            nameof(TopFunding.EarlyYears),
            nameof(PlaceFunding.Primary)
        ];

        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsLocalAuthorityAsync(codes.First(), cancellationToken);
        if (years == null)
        {
            return null;
        }

        var laBuilder = new LocalAuthorityFinancialDefaultQuery(dimension, fields)
            .WhereLaCodesIn(codes)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        var results = (await conn.QueryAsync(laBuilder, types, Mapper.MultiMapToHighNeedsYear, splitOn, cancellationToken)).ToArray();
        return new History<HighNeedsYear>
        {
            StartYear = years.StartYear + 1,
            EndYear = years.EndYear,
            Outturn = results.Select(r => r.outturn).ToArray(),
            Budget = results.Select(r => r.budget).ToArray()
        };
    }
}