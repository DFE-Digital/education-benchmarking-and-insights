using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenASchoolCustomDataChangeViewModel
{
    private readonly Census _census;
    private readonly CustomData _currentValues;
    private readonly CustomData _customInput;
    private readonly SchoolExpenditure _expenditure;
    private readonly Finances _finances;
    private readonly Fixture _fixture = new();
    private readonly FloorAreaMetric _floorArea;
    private readonly Income _income;
    private readonly School _school;
    private readonly SchoolCustomDataChangeViewModel _sut;

    public GivenASchoolCustomDataChangeViewModel()
    {
        _school = new School();
        _finances = _fixture.Create<Finances>();
        _income = _fixture.Create<Income>();
        _expenditure = _fixture.Create<SchoolExpenditure>();
        _census = _fixture.Create<Census>();
        _floorArea = _fixture.Create<FloorAreaMetric>();
        _currentValues = new CustomData(_finances, _income, _expenditure, _census, _floorArea);
        _customInput = _fixture.Create<CustomData>();
        _sut = new SchoolCustomDataChangeViewModel(_school, _currentValues, _customInput);
    }

    [Fact]
    public void SchoolIsSet()
    {
        Assert.Equal(_school, _sut.School);
    }

    [Fact]
    public void CurrentValuesIsSet()
    {
        // Administrative supplies
        Assert.Equal(_expenditure.AdministrativeSuppliesCosts, _sut.CurrentValues.AdministrativeSuppliesCosts);

        // Catering
        Assert.Equal(_expenditure.CateringStaffCosts, _sut.CurrentValues.CateringStaffCosts);
        Assert.Equal(_expenditure.CateringSuppliesCosts, _sut.CurrentValues.CateringSuppliesCosts);
        Assert.Equal(_income.IncomeCatering, _sut.CurrentValues.CateringIncome);

        // Educational supplies
        Assert.Equal(_expenditure.ExaminationFeesCosts, _sut.CurrentValues.ExaminationFeesCosts);
        Assert.Equal(_expenditure.LearningResourcesNonIctCosts, _sut.CurrentValues.LearningResourcesNonIctCosts);

        // IT
        Assert.Equal(_expenditure.LearningResourcesIctCosts, _sut.CurrentValues.LearningResourcesIctCosts);

        // Non-educational support staff
        Assert.Equal(_expenditure.AdministrativeClericalStaffCosts,
            _sut.CurrentValues.AdministrativeClericalStaffCosts);
        Assert.Equal(_expenditure.AuditorsCosts, _sut.CurrentValues.AuditorsCosts);
        Assert.Equal(_expenditure.OtherStaffCosts, _sut.CurrentValues.OtherStaffCosts);
        Assert.Equal(_expenditure.ProfessionalServicesNonCurriculumCosts,
            _sut.CurrentValues.ProfessionalServicesNonCurriculumCosts);

        // Premises and services
        Assert.Equal(_expenditure.CleaningCaretakingCosts, _sut.CurrentValues.CleaningCaretakingCosts);
        Assert.Equal(_expenditure.MaintenancePremisesCosts, _sut.CurrentValues.MaintenancePremisesCosts);
        Assert.Equal(_expenditure.OtherOccupationCosts, _sut.CurrentValues.OtherOccupationCosts);
        Assert.Equal(_expenditure.PremisesStaffCosts, _sut.CurrentValues.PremisesStaffCosts);

        // Teaching and teaching support
        Assert.Equal(_expenditure.AgencySupplyTeachingStaffCosts, _sut.CurrentValues.AgencySupplyTeachingStaffCosts);
        Assert.Equal(_expenditure.EducationSupportStaffCosts, _sut.CurrentValues.EducationSupportStaffCosts);
        Assert.Equal(_expenditure.EducationalConsultancyCosts, _sut.CurrentValues.EducationalConsultancyCosts);
        Assert.Equal(_expenditure.SupplyTeachingStaffCosts, _sut.CurrentValues.SupplyTeachingStaffCosts);
        Assert.Equal(_expenditure.TeachingStaffCosts, _sut.CurrentValues.TeachingStaffCosts);

        // Utilities
        Assert.Equal(_expenditure.EnergyCosts, _sut.CurrentValues.EnergyCosts);
        Assert.Equal(_expenditure.WaterSewerageCosts, _sut.CurrentValues.WaterSewerageCosts);

        // Other costs
        Assert.Equal(_expenditure.DirectRevenueFinancingCosts, _sut.CurrentValues.DirectRevenueFinancingCosts);
        Assert.Equal(_expenditure.GroundsMaintenanceCosts, _sut.CurrentValues.GroundsMaintenanceCosts);
        Assert.Equal(_expenditure.IndirectEmployeeExpenses, _sut.CurrentValues.IndirectEmployeeExpenses);
        Assert.Equal(_expenditure.InterestChargesLoanBank, _sut.CurrentValues.InterestChargesLoanBank);
        Assert.Equal(_expenditure.OtherInsurancePremiumsCosts, _sut.CurrentValues.OtherInsurancePremiumsCosts);
        Assert.Equal(_expenditure.PrivateFinanceInitiativeCharges, _sut.CurrentValues.PrivateFinanceInitiativeCharges);
        Assert.Equal(_expenditure.RentRatesCosts, _sut.CurrentValues.RentRatesCosts);
        Assert.Equal(_expenditure.SpecialFacilitiesCosts, _sut.CurrentValues.SpecialFacilitiesCosts);
        Assert.Equal(_expenditure.StaffDevelopmentTrainingCosts, _sut.CurrentValues.StaffDevelopmentTrainingCosts);
        Assert.Equal(_expenditure.StaffRelatedInsuranceCosts, _sut.CurrentValues.StaffRelatedInsuranceCosts);
        Assert.Equal(_expenditure.SupplyTeacherInsurableCosts, _sut.CurrentValues.SupplyTeacherInsurableCosts);

        // Totals
        Assert.Equal(_finances.TotalIncome, _sut.CurrentValues.TotalIncome);
        Assert.Equal(_finances.TotalExpenditure, _sut.CurrentValues.TotalExpenditure);
        Assert.Equal(_finances.RevenueReserve, _sut.CurrentValues.RevenueReserve);

        // Non-financial data
        Assert.Equal(_census.TotalPupils, _sut.CurrentValues.NumberOfPupilsFte);
        Assert.Equal(_finances.FreeSchoolMealPercent, _sut.CurrentValues.FreeSchoolMealPercent);
        Assert.Equal(_finances.SpecialEducationalNeedsPercent, _sut.CurrentValues.SpecialEducationalNeedsPercent);
        Assert.Equal(_floorArea.FloorArea, _sut.CurrentValues.FloorArea);

        // Workforce data
        Assert.Equal(_census.WorkforceFTE, _sut.CurrentValues.WorkforceFte);
        Assert.Equal(_census.TeachersFTE, _sut.CurrentValues.TeachersFte);
        Assert.Equal(_census.SeniorLeadershipFTE, _sut.CurrentValues.SeniorLeadershipFte);
    }

    [Fact]
    public void CustomInputIsSet()
    {
        // Administrative supplies
        Assert.Equal(_customInput.AdministrativeSuppliesCosts, _sut.CustomInput.AdministrativeSuppliesCosts);

        // Catering
        Assert.Equal(_customInput.CateringStaffCosts, _sut.CustomInput.CateringStaffCosts);
        Assert.Equal(_customInput.CateringSuppliesCosts, _sut.CustomInput.CateringSuppliesCosts);
        Assert.Equal(_customInput.CateringIncome, _sut.CustomInput.CateringIncome);

        // Educational supplies
        Assert.Equal(_customInput.ExaminationFeesCosts, _sut.CustomInput.ExaminationFeesCosts);
        Assert.Equal(_customInput.LearningResourcesNonIctCosts, _sut.CustomInput.LearningResourcesNonIctCosts);

        // IT
        Assert.Equal(_customInput.LearningResourcesIctCosts, _sut.CustomInput.LearningResourcesIctCosts);

        // Non-educational support staff
        Assert.Equal(_customInput.AdministrativeClericalStaffCosts, _sut.CustomInput.AdministrativeClericalStaffCosts);
        Assert.Equal(_customInput.AuditorsCosts, _sut.CustomInput.AuditorsCosts);
        Assert.Equal(_customInput.OtherStaffCosts, _sut.CustomInput.OtherStaffCosts);
        Assert.Equal(_customInput.ProfessionalServicesNonCurriculumCosts,
            _sut.CustomInput.ProfessionalServicesNonCurriculumCosts);

        // Premises and services
        Assert.Equal(_customInput.CleaningCaretakingCosts, _sut.CustomInput.CleaningCaretakingCosts);
        Assert.Equal(_customInput.MaintenancePremisesCosts, _sut.CustomInput.MaintenancePremisesCosts);
        Assert.Equal(_customInput.OtherOccupationCosts, _sut.CustomInput.OtherOccupationCosts);
        Assert.Equal(_customInput.PremisesStaffCosts, _sut.CustomInput.PremisesStaffCosts);

        // Teaching and teaching support
        Assert.Equal(_customInput.AgencySupplyTeachingStaffCosts, _sut.CustomInput.AgencySupplyTeachingStaffCosts);
        Assert.Equal(_customInput.EducationSupportStaffCosts, _sut.CustomInput.EducationSupportStaffCosts);
        Assert.Equal(_customInput.EducationalConsultancyCosts, _sut.CustomInput.EducationalConsultancyCosts);
        Assert.Equal(_customInput.SupplyTeachingStaffCosts, _sut.CustomInput.SupplyTeachingStaffCosts);
        Assert.Equal(_customInput.TeachingStaffCosts, _sut.CustomInput.TeachingStaffCosts);

        // Utilities
        Assert.Equal(_customInput.EnergyCosts, _sut.CustomInput.EnergyCosts);
        Assert.Equal(_customInput.WaterSewerageCosts, _sut.CustomInput.WaterSewerageCosts);

        // Other costs
        Assert.Equal(_customInput.DirectRevenueFinancingCosts, _sut.CustomInput.DirectRevenueFinancingCosts);
        Assert.Equal(_customInput.GroundsMaintenanceCosts, _sut.CustomInput.GroundsMaintenanceCosts);
        Assert.Equal(_customInput.IndirectEmployeeExpenses, _sut.CustomInput.IndirectEmployeeExpenses);
        Assert.Equal(_customInput.InterestChargesLoanBank, _sut.CustomInput.InterestChargesLoanBank);
        Assert.Equal(_customInput.OtherInsurancePremiumsCosts, _sut.CustomInput.OtherInsurancePremiumsCosts);
        Assert.Equal(_customInput.PrivateFinanceInitiativeCharges, _sut.CustomInput.PrivateFinanceInitiativeCharges);
        Assert.Equal(_customInput.RentRatesCosts, _sut.CustomInput.RentRatesCosts);
        Assert.Equal(_customInput.SpecialFacilitiesCosts, _sut.CustomInput.SpecialFacilitiesCosts);
        Assert.Equal(_customInput.StaffDevelopmentTrainingCosts, _sut.CustomInput.StaffDevelopmentTrainingCosts);
        Assert.Equal(_customInput.StaffRelatedInsuranceCosts, _sut.CustomInput.StaffRelatedInsuranceCosts);
        Assert.Equal(_customInput.SupplyTeacherInsurableCosts, _sut.CustomInput.SupplyTeacherInsurableCosts);

        // Totals
        Assert.Equal(_customInput.TotalIncome, _sut.CustomInput.TotalIncome);
        Assert.Equal(_customInput.TotalExpenditure, _sut.CustomInput.TotalExpenditure);
        Assert.Equal(_customInput.RevenueReserve, _sut.CustomInput.RevenueReserve);

        // Non-financial data
        Assert.Equal(_customInput.NumberOfPupilsFte, _sut.CustomInput.NumberOfPupilsFte);
        Assert.Equal(_customInput.FreeSchoolMealPercent, _sut.CustomInput.FreeSchoolMealPercent);
        Assert.Equal(_customInput.SpecialEducationalNeedsPercent, _sut.CustomInput.SpecialEducationalNeedsPercent);
        Assert.Equal(_customInput.FloorArea, _sut.CustomInput.FloorArea);

        // Workforce data
        Assert.Equal(_customInput.WorkforceFte, _sut.CustomInput.WorkforceFte);
        Assert.Equal(_customInput.TeachersFte, _sut.CustomInput.TeachersFte);
        Assert.Equal(_customInput.SeniorLeadershipFte, _sut.CustomInput.SeniorLeadershipFte);
    }

    [Fact]
    public void AdministrativeSuppliesSectionIsSet()
    {
        var administrativeSuppliesCosts = _sut.AdministrativeSuppliesSection.Values.ElementAtOrDefault(0);
        Assert.NotNull(administrativeSuppliesCosts);
        Assert.Equal(_currentValues.AdministrativeSuppliesCosts, administrativeSuppliesCosts.Current);
        Assert.Equal(_customInput.AdministrativeSuppliesCosts, administrativeSuppliesCosts.Custom);
    }
}