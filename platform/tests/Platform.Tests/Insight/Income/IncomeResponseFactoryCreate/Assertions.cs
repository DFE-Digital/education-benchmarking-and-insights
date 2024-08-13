using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

public static class Assertions
{
    internal static void AssertTotalIncome(IncomeBaseResponse expected, IncomeBaseResponse response)
    {
        AssertEqual(nameof(IncomeBaseResponse.TotalIncome), expected.TotalIncome, response.TotalIncome);
        AssertEqual(nameof(IncomeBaseResponse.SchoolTotalIncome), expected.SchoolTotalIncome, response.SchoolTotalIncome);
        AssertEqual(nameof(IncomeBaseResponse.CentralTotalIncome), expected.CentralTotalIncome, response.CentralTotalIncome);
    }

    internal static void AssertGrantFunding(IncomeBaseResponse expected, IncomeBaseResponse response)
    {
        AssertEqual(nameof(IncomeBaseResponse.TotalGrantFunding), expected.TotalGrantFunding, response.TotalGrantFunding);
        AssertEqual(nameof(IncomeBaseResponse.SchoolTotalGrantFunding), expected.SchoolTotalGrantFunding, response.SchoolTotalGrantFunding);
        AssertEqual(nameof(IncomeBaseResponse.CentralTotalGrantFunding), expected.CentralTotalGrantFunding, response.CentralTotalGrantFunding);

        AssertEqual(nameof(IncomeBaseResponse.DirectGrants), expected.DirectGrants, response.DirectGrants);
        AssertEqual(nameof(IncomeBaseResponse.SchoolDirectGrants), expected.SchoolDirectGrants, response.SchoolDirectGrants);
        AssertEqual(nameof(IncomeBaseResponse.CentralDirectGrants), expected.CentralDirectGrants, response.CentralDirectGrants);

        AssertEqual(nameof(IncomeBaseResponse.PrePost16Funding), expected.PrePost16Funding, response.PrePost16Funding);
        AssertEqual(nameof(IncomeBaseResponse.SchoolPrePost16Funding), expected.SchoolPrePost16Funding, response.SchoolPrePost16Funding);
        AssertEqual(nameof(IncomeBaseResponse.CentralPrePost16Funding), expected.CentralPrePost16Funding, response.CentralPrePost16Funding);

        AssertEqual(nameof(IncomeBaseResponse.OtherDfeGrants), expected.OtherDfeGrants, response.OtherDfeGrants);
        AssertEqual(nameof(IncomeBaseResponse.SchoolOtherDfeGrants), expected.SchoolOtherDfeGrants, response.SchoolOtherDfeGrants);
        AssertEqual(nameof(IncomeBaseResponse.CentralOtherDfeGrants), expected.CentralOtherDfeGrants, response.CentralOtherDfeGrants);

        AssertEqual(nameof(IncomeBaseResponse.OtherIncomeGrants), expected.OtherIncomeGrants, response.OtherIncomeGrants);
        AssertEqual(nameof(IncomeBaseResponse.SchoolOtherIncomeGrants), expected.SchoolOtherIncomeGrants, response.SchoolOtherIncomeGrants);
        AssertEqual(nameof(IncomeBaseResponse.CentralOtherIncomeGrants), expected.CentralOtherIncomeGrants, response.CentralOtherIncomeGrants);

        AssertEqual(nameof(IncomeBaseResponse.GovernmentSource), expected.GovernmentSource, response.GovernmentSource);
        AssertEqual(nameof(IncomeBaseResponse.SchoolGovernmentSource), expected.SchoolGovernmentSource, response.SchoolGovernmentSource);
        AssertEqual(nameof(IncomeBaseResponse.CentralGovernmentSource), expected.CentralGovernmentSource, response.CentralGovernmentSource);

        AssertEqual(nameof(IncomeBaseResponse.CommunityGrants), expected.CommunityGrants, response.CommunityGrants);
        AssertEqual(nameof(IncomeBaseResponse.SchoolCommunityGrants), expected.SchoolCommunityGrants, response.SchoolCommunityGrants);
        AssertEqual(nameof(IncomeBaseResponse.CentralCommunityGrants), expected.CentralCommunityGrants, response.CentralCommunityGrants);

        AssertEqual(nameof(IncomeBaseResponse.Academies), expected.Academies, response.Academies);
        AssertEqual(nameof(IncomeBaseResponse.SchoolAcademies), expected.SchoolAcademies, response.SchoolAcademies);
        AssertEqual(nameof(IncomeBaseResponse.CentralAcademies), expected.CentralAcademies, response.CentralAcademies);
    }

    internal static void AssertSelfGenerated(IncomeBaseResponse expected, IncomeBaseResponse response)
    {
        AssertEqual(nameof(IncomeBaseResponse.TotalSelfGeneratedFunding), expected.TotalSelfGeneratedFunding, response.TotalSelfGeneratedFunding);
        AssertEqual(nameof(IncomeBaseResponse.SchoolTotalSelfGeneratedFunding), expected.SchoolTotalSelfGeneratedFunding, response.SchoolTotalSelfGeneratedFunding);
        AssertEqual(nameof(IncomeBaseResponse.CentralTotalSelfGeneratedFunding), expected.CentralTotalSelfGeneratedFunding, response.CentralTotalSelfGeneratedFunding);

        AssertEqual(nameof(IncomeBaseResponse.IncomeFacilitiesServices), expected.IncomeFacilitiesServices, response.IncomeFacilitiesServices);
        AssertEqual(nameof(IncomeBaseResponse.SchoolIncomeFacilitiesServices), expected.SchoolIncomeFacilitiesServices, response.SchoolIncomeFacilitiesServices);
        AssertEqual(nameof(IncomeBaseResponse.CentralIncomeFacilitiesServices), expected.CentralIncomeFacilitiesServices, response.CentralIncomeFacilitiesServices);

        AssertEqual(nameof(IncomeBaseResponse.IncomeCatering), expected.IncomeCatering, response.IncomeCatering);
        AssertEqual(nameof(IncomeBaseResponse.SchoolIncomeCatering), expected.SchoolIncomeCatering, response.SchoolIncomeCatering);
        AssertEqual(nameof(IncomeBaseResponse.CentralIncomeCatering), expected.CentralIncomeCatering, response.CentralIncomeCatering);

        AssertEqual(nameof(IncomeBaseResponse.DonationsVoluntaryFunds), expected.DonationsVoluntaryFunds, response.DonationsVoluntaryFunds);
        AssertEqual(nameof(IncomeBaseResponse.SchoolDonationsVoluntaryFunds), expected.SchoolDonationsVoluntaryFunds, response.SchoolDonationsVoluntaryFunds);
        AssertEqual(nameof(IncomeBaseResponse.CentralDonationsVoluntaryFunds), expected.CentralDonationsVoluntaryFunds, response.CentralDonationsVoluntaryFunds);

        AssertEqual(nameof(IncomeBaseResponse.ReceiptsSupplyTeacherInsuranceClaims), expected.ReceiptsSupplyTeacherInsuranceClaims, response.ReceiptsSupplyTeacherInsuranceClaims);
        AssertEqual(nameof(IncomeBaseResponse.SchoolReceiptsSupplyTeacherInsuranceClaims), expected.SchoolReceiptsSupplyTeacherInsuranceClaims, response.SchoolReceiptsSupplyTeacherInsuranceClaims);
        AssertEqual(nameof(IncomeBaseResponse.CentralReceiptsSupplyTeacherInsuranceClaims), expected.CentralReceiptsSupplyTeacherInsuranceClaims, response.CentralReceiptsSupplyTeacherInsuranceClaims);

        AssertEqual(nameof(IncomeBaseResponse.InvestmentIncome), expected.InvestmentIncome, response.InvestmentIncome);
        AssertEqual(nameof(IncomeBaseResponse.SchoolInvestmentIncome), expected.SchoolInvestmentIncome, response.SchoolInvestmentIncome);
        AssertEqual(nameof(IncomeBaseResponse.CentralInvestmentIncome), expected.CentralInvestmentIncome, response.CentralInvestmentIncome);

        AssertEqual(nameof(IncomeBaseResponse.OtherSelfGeneratedIncome), expected.OtherSelfGeneratedIncome, response.OtherSelfGeneratedIncome);
        AssertEqual(nameof(IncomeBaseResponse.SchoolOtherSelfGeneratedIncome), expected.SchoolOtherSelfGeneratedIncome, response.SchoolOtherSelfGeneratedIncome);
        AssertEqual(nameof(IncomeBaseResponse.CentralOtherSelfGeneratedIncome), expected.CentralOtherSelfGeneratedIncome, response.CentralOtherSelfGeneratedIncome);
    }

    internal static void AssertDirectRevenueFinancing(IncomeBaseResponse expected, IncomeBaseResponse response)
    {
        AssertEqual(nameof(IncomeBaseResponse.DirectRevenueFinancing), expected.DirectRevenueFinancing, response.DirectRevenueFinancing);
        AssertEqual(nameof(IncomeBaseResponse.SchoolDirectRevenueFinancing), expected.SchoolDirectRevenueFinancing, response.SchoolDirectRevenueFinancing);
        AssertEqual(nameof(IncomeBaseResponse.CentralDirectRevenueFinancing), expected.CentralDirectRevenueFinancing, response.CentralDirectRevenueFinancing);
    }

    private static void AssertEqual(string field, decimal? expected, decimal? actual) =>
        Assert.True(
            Math.Abs(expected.GetValueOrDefault() - actual.GetValueOrDefault()) < 0.02m,
            $"Expected `{expected}` for {field} but got `{actual}`");
}