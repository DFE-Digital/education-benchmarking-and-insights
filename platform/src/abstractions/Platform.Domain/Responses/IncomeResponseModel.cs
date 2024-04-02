using System;

namespace Platform.Domain;

public class IncomeResponseModel
{
    public int YearEnd { get; private set; }
    public Dimension Dimension { get; private set; }
    public decimal? TotalIncome { get; private set; }
    public decimal? TotalGrantFunding { get; private set; }
    public decimal? TotalSelfGeneratedFunding { get; private set; }
    public decimal? DirectRevenueFinancing { get; private set; }
    public decimal? DirectGrants { get; private set; }
    public decimal? PrePost16Funding { get; private set; }
    public decimal? OtherDfeGrants { get; private set; }
    public decimal? OtherIncomeGrants { get; private set; }
    public decimal? GovernmentSource { get; private set; }
    public decimal? CommunityGrants { get; private set; }
    public decimal? Academies  { get; private set; }
    public decimal? IncomeFacilitiesServices  { get; private set; }
    public decimal? IncomeCatering  { get; private set; }
    public decimal? DonationsVoluntaryFunds  { get; private set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaims  { get; private set; }
    public decimal? InvestmentIncome  { get; private set; }
    public decimal? OtherSelfGeneratedIncome  { get; private set; }

    public static IncomeResponseModel Create(SchoolTrustFinancialDataObject? dataObject, int term, Dimension dimension = Dimension.Actuals)
    {
        return dataObject is null
            ? new IncomeResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
            }
            : new IncomeResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
                TotalIncome = CalculationValue(dataObject.TotalIncome, dataObject, dimension),
                TotalGrantFunding = CalculationValue(dataObject.GrantFunding, dataObject, dimension),
                TotalSelfGeneratedFunding = CalculationValue(dataObject.SelfGeneratedFunding, dataObject, dimension),
                DirectRevenueFinancing = CalculationValue(dataObject.DirectRevenueFinancing, dataObject, dimension),
                DirectGrants = CalculationValue(dataObject.DirectGrant, dataObject, dimension),
                PrePost16Funding = CalculationValue(dataObject.PrePost16Funding, dataObject, dimension),
                OtherDfeGrants = CalculationValue(dataObject.OtherDfeGrants, dataObject, dimension),
                OtherIncomeGrants = CalculationValue(dataObject.OtherIncomeGrants, dataObject, dimension),
                GovernmentSource = CalculationValue(dataObject.GovernmentSource, dataObject, dimension),
                CommunityGrants = CalculationValue(dataObject.CommunityGrants, dataObject, dimension),
                Academies = CalculationValue(dataObject.Academies, dataObject, dimension),
                IncomeFacilitiesServices = CalculationValue(dataObject.IncomeFromFacilities, dataObject, dimension),
                IncomeCatering = CalculationValue(dataObject.IncomeFromCatering, dataObject, dimension),
                DonationsVoluntaryFunds = CalculationValue(dataObject.Donations, dataObject, dimension),
                ReceiptsSupplyTeacherInsuranceClaims = CalculationValue(dataObject.ReceiptsFromSupply, dataObject, dimension),
                InvestmentIncome = CalculationValue(dataObject.InvestmentIncome, dataObject, dimension),
                OtherSelfGeneratedIncome = CalculationValue(dataObject.OtherSelfGenerated, dataObject, dimension)
            };
    }

    private static decimal CalculationValue(decimal value, SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return dimension switch
        {
            Dimension.Actuals => value,
            Dimension.PoundPerPupil => dataObject.NoPupils != 0 ? value / dataObject.NoPupils : 0,
            Dimension.PercentIncome => dataObject.TotalIncome != 0 ? value / dataObject.TotalIncome * 100 : 0,
            Dimension.PercentExpenditure => dataObject.TotalExpenditure != 0 ? value / dataObject.TotalExpenditure * 100 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}