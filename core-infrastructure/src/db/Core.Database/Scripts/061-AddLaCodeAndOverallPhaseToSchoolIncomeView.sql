ALTER VIEW [dbo].[SchoolIncome] AS
SELECT
   s.URN,
   s.SchoolName,
   s.SchoolType,
   s.OverallPhase,
   s.LACode,
   s.LAName,
   s.TrustCompanyNumber,
   f.PeriodCoveredByReturn,
   f.TotalPupils,
   f.TotalIncome,
   f.TotalExpenditure,
   f.TotalGrantFunding,
   f.TotalSelfGeneratedFunding,
   f.DirectRevenueFinancing,
   f.DirectGrants,
   f.PrePost16Funding,
   f.OtherDfeGrants,
   f.OtherIncomeGrants,
   f.GovernmentSource,
   f.CommunityGrants,
   f.Academies,
   f.IncomeFacilitiesServices,
   f.IncomeCateringServices,
   f.DonationsVoluntaryFunds,
   f.ReceiptsSupplyTeacherInsuranceClaims,
   f.InvestmentIncome,
   f.OtherSelfGeneratedIncome,
   f.TotalIncomeCS,
   f.TotalExpenditureCS,
   f.TotalGrantFundingCS,
   f.TotalSelfGeneratedFundingCS,
   f.DirectRevenueFinancingCS,
   f.DirectGrantsCS,
   f.PrePost16FundingCS,
   f.OtherDfeGrantsCS,
   f.OtherIncomeGrantsCS,
   f.GovernmentSourceCS,
   f.CommunityGrantsCS,
   f.AcademiesCS,
   f.IncomeFacilitiesServicesCS,
   f.IncomeCateringServicesCS,
   f.DonationsVoluntaryFundsCS,
   f.ReceiptsSupplyTeacherInsuranceClaimsCS,
   f.InvestmentIncomeCS,
   f.OtherSelfGeneratedIncomeCS
FROM
   School s
   LEFT JOIN CurrentDefaultFinancial f ON f.URN = s.URN