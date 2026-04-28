using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.Accounts.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.Accounts.Services;

public interface IHighNeedsService
{
    Task<LocalAuthority<HighNeeds>[]> QueryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<History<HighNeedsYear>?> QueryHistoryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<LocalAuthority<HighNeeds>[]> QueryByTransactionTypeAsync(string[] codes, string dimension, string type, CancellationToken cancellationToken = default);
}

public class HighNeedsService(IDatabaseFactory dbFactory) : IHighNeedsService
{
    public async Task<LocalAuthority<HighNeeds>[]> QueryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        string[] fields =
        [
            // LocalAuthorityBase
            "LaCode AS [Code]",
            "[Name]",
            "[Population2To18]",
            "[TotalPupils]",
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

    public async Task<History<HighNeedsYear>?> QueryHistoryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default)
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
        var years = await QueryYearsLocalAuthorityAsync(conn, codes.First(), cancellationToken);
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
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Outturn = results.Select(r => r.outturn).ToArray(),
            Budget = results.Select(r => r.budget).ToArray()
        };
    }

    public async Task<LocalAuthority<HighNeeds>[]> QueryByTransactionTypeAsync(string[] codes, string dimension, string type, CancellationToken cancellationToken = default)
    {
        string[] budgetFields =
        [
            Field(HighNeedsFields.LaCode, nameof(LocalAuthorityBase.Code)),
            nameof(HighNeedsFields.Name),
            nameof(HighNeedsFields.Population2To18),
            nameof(HighNeedsFields.TotalPupils),
            Field(HighNeedsFields.BudgetTotalHighNeeds, nameof(HighNeedsBase.Total)),
            Field(HighNeedsFields.BudgetTotalPlaceFunding, nameof(HighNeedsAmount.TotalPlaceFunding)),
            Field(HighNeedsFields.BudgetTotalTopUpFundingMaintained, nameof(HighNeedsAmount.TopUpFundingMaintained)),
            Field(HighNeedsFields.BudgetTotalTopUpFundingNonMaintained, nameof(HighNeedsAmount.TopUpFundingNonMaintained)),
            Field(HighNeedsFields.BudgetTotalSenServices, nameof(HighNeedsAmount.SenServices)),
            Field(HighNeedsFields.BudgetTotalAlternativeProvisionServices, nameof(HighNeedsAmount.AlternativeProvisionServices)),
            Field(HighNeedsFields.BudgetTotalHospitalServices, nameof(HighNeedsAmount.HospitalServices)),
            Field(HighNeedsFields.BudgetTotalOtherHealthServices, nameof(HighNeedsAmount.OtherHealthServices)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedEarlyYears, nameof(TopFunding.EarlyYears)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedPrimary, nameof(TopFunding.Primary)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedSecondary, nameof(TopFunding.Secondary)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedSpecial, nameof(TopFunding.Special)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedAlternativeProvision, nameof(TopFunding.AlternativeProvision)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedPostSchool, nameof(TopFunding.PostSchool)),
            Field(HighNeedsFields.BudgetTopFundingMaintainedIncome, nameof(TopFunding.Income)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedEarlyYears, nameof(TopFunding.EarlyYears)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedPrimary, nameof(TopFunding.Primary)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedSecondary, nameof(TopFunding.Secondary)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedSpecial, nameof(TopFunding.Special)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedAlternativeProvision, nameof(TopFunding.AlternativeProvision)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedPostSchool, nameof(TopFunding.PostSchool)),
            Field(HighNeedsFields.BudgetTopFundingNonMaintainedIncome, nameof(TopFunding.Income)),
            Field(HighNeedsFields.BudgetPlaceFundingPrimary, nameof(PlaceFunding.Primary)),
            Field(HighNeedsFields.BudgetPlaceFundingSecondary, nameof(PlaceFunding.Secondary)),
            Field(HighNeedsFields.BudgetPlaceFundingSpecial, nameof(PlaceFunding.Special)),
            Field(HighNeedsFields.BudgetPlaceFundingAlternativeProvision, nameof(PlaceFunding.AlternativeProvision)),
            Field(HighNeedsFields.BudgetSENTransportDSG, nameof(SenTransport.SenTransportDsg)),
            Field(HighNeedsFields.BudgetHometoSchoolTransportPre16, nameof(SenTransport.HomeToSchoolTransportPre16)),
            Field(HighNeedsFields.BudgetHometoSchoolTransport1618, nameof(SenTransport.HomeToSchoolTransport16To18)),
            Field(HighNeedsFields.BudgetHometoSchoolTransport1925, nameof(SenTransport.HomeToSchoolTransport19To25)),
            Field(HighNeedsFields.BudgetEdPsychologyService, nameof(CentralSenServices.EdPsychologyService)),
            Field(HighNeedsFields.BudgetSENAdmin, nameof(CentralSenServices.SenAdmin))
        ];

        string[] outturnFields =
        [
            Field(HighNeedsFields.LaCode, nameof(LocalAuthorityBase.Code)),
            nameof(HighNeedsFields.Name),
            nameof(HighNeedsFields.Population2To18),
            nameof(HighNeedsFields.TotalPupils),
            Field(HighNeedsFields.OutturnTotalHighNeeds, nameof(HighNeedsBase.Total)),
            Field(HighNeedsFields.OutturnTotalPlaceFunding, nameof(HighNeedsAmount.TotalPlaceFunding)),
            Field(HighNeedsFields.OutturnTotalTopUpFundingMaintained, nameof(HighNeedsAmount.TopUpFundingMaintained)),
            Field(HighNeedsFields.OutturnTotalTopUpFundingNonMaintained, nameof(HighNeedsAmount.TopUpFundingNonMaintained)),
            Field(HighNeedsFields.OutturnTotalSenServices, nameof(HighNeedsAmount.SenServices)),
            Field(HighNeedsFields.OutturnTotalAlternativeProvisionServices, nameof(HighNeedsAmount.AlternativeProvisionServices)),
            Field(HighNeedsFields.OutturnTotalHospitalServices, nameof(HighNeedsAmount.HospitalServices)),
            Field(HighNeedsFields.OutturnTotalOtherHealthServices, nameof(HighNeedsAmount.OtherHealthServices)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedEarlyYears, nameof(TopFunding.EarlyYears)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedPrimary, nameof(TopFunding.Primary)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedSecondary, nameof(TopFunding.Secondary)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedSpecial, nameof(TopFunding.Special)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedAlternativeProvision, nameof(TopFunding.AlternativeProvision)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedPostSchool, nameof(TopFunding.PostSchool)),
            Field(HighNeedsFields.OutturnTopFundingMaintainedIncome, nameof(TopFunding.Income)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedEarlyYears, nameof(TopFunding.EarlyYears)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedPrimary, nameof(TopFunding.Primary)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedSecondary, nameof(TopFunding.Secondary)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedSpecial, nameof(TopFunding.Special)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedAlternativeProvision, nameof(TopFunding.AlternativeProvision)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedPostSchool, nameof(TopFunding.PostSchool)),
            Field(HighNeedsFields.OutturnTopFundingNonMaintainedIncome, nameof(TopFunding.Income)),
            Field(HighNeedsFields.OutturnPlaceFundingPrimary, nameof(PlaceFunding.Primary)),
            Field(HighNeedsFields.OutturnPlaceFundingSecondary, nameof(PlaceFunding.Secondary)),
            Field(HighNeedsFields.OutturnPlaceFundingSpecial, nameof(PlaceFunding.Special)),
            Field(HighNeedsFields.OutturnPlaceFundingAlternativeProvision, nameof(PlaceFunding.AlternativeProvision)),
            Field(HighNeedsFields.OutturnSENTransportDSG, nameof(SenTransport.SenTransportDsg)),
            Field(HighNeedsFields.OutturnHometoSchoolTransportPre16, nameof(SenTransport.HomeToSchoolTransportPre16)),
            Field(HighNeedsFields.OutturnHometoSchoolTransport1618, nameof(SenTransport.HomeToSchoolTransport16To18)),
            Field(HighNeedsFields.OutturnHometoSchoolTransport1925, nameof(SenTransport.HomeToSchoolTransport19To25)),
            Field(HighNeedsFields.OutturnEdPsychologyService, nameof(CentralSenServices.EdPsychologyService)),
            Field(HighNeedsFields.OutturnSENAdmin, nameof(CentralSenServices.SenAdmin))
        ];

        var fields = type == "outturn" ? outturnFields : budgetFields;

        Type[] types =
        [
            typeof(LocalAuthorityBase),
            typeof(HighNeedsBase),
            typeof(HighNeedsAmount),
            typeof(TopFunding),
            typeof(TopFunding),
            typeof(PlaceFunding),
            typeof(SenTransport),
            typeof(CentralSenServices)
        ];

        string[] splitOn =
        [
            nameof(HighNeedsBase.Total),
            nameof(HighNeedsAmount.TotalPlaceFunding),
            nameof(TopFunding.EarlyYears),
            nameof(TopFunding.EarlyYears),
            nameof(PlaceFunding.Primary),
            nameof(SenTransport.SenTransportDsg),
            nameof(CentralSenServices.EdPsychologyService)
        ];

        using var conn = await dbFactory.GetConnection();
        var laBuilder = new LocalAuthorityFinancialDefaultCurrentQuery(dimension, fields)
            .WhereLaCodesIn(codes);

        var results = await conn.QueryAsync(laBuilder, types, Mapper.MultiMapToHighNeedsByTransactionType, splitOn, cancellationToken);
        return results.ToArray();
    }

    private static async Task<YearsModelDto?> QueryYearsLocalAuthorityAsync(IDatabaseConnection conn, string code, CancellationToken cancellationToken = default)
    {
        var builder = new YearsLocalAuthorityQuery(code);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }

    private static string Field(string source, string dest) => $"{source} AS [{dest}]";
}