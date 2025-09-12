using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataChangeViewModel(
    School school,
    CustomData currentValues,
    CustomData customInput
)
{
    public School School { get; } = school;
    public SchoolCustomDataViewModel CurrentValues { get; } = SchoolCustomDataViewModel.FromCustomData(currentValues);
    public SchoolCustomDataViewModel CustomInput { get; } = SchoolCustomDataViewModel.FromCustomData(customInput);

    public SchoolCustomDataSectionViewModel AdministrativeSuppliesSection => new(
        "Administrative supplies",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.AdministrativeSupplies.AdministrativeSuppliesNonEducationalCosts,
            Name = nameof(SchoolCustomDataViewModel.AdministrativeSuppliesNonEducationalCosts),
            Current = CurrentValues.AdministrativeSuppliesNonEducationalCosts,
            Custom = CustomInput.AdministrativeSuppliesNonEducationalCosts
        }
    );

    public SchoolCustomDataSectionViewModel CateringSection => new(
        "Catering",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.CateringStaffServices.CateringStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.CateringStaffCosts),
            Current = CurrentValues.CateringStaffCosts,
            Custom = CustomInput.CateringStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.CateringStaffServices.CateringSuppliesCosts,
            Name = nameof(SchoolCustomDataViewModel.CateringSuppliesCosts),
            Current = CurrentValues.CateringSuppliesCosts,
            Custom = CustomInput.CateringSuppliesCosts
        }
    );

    public SchoolCustomDataSectionViewModel EducationalSuppliesSection => new(
        "Educational supplies",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.EducationalSupplies.ExaminationFeesCosts,
            Name = nameof(SchoolCustomDataViewModel.ExaminationFeesCosts),
            Current = CurrentValues.ExaminationFeesCosts,
            Custom = CustomInput.ExaminationFeesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.EducationalSupplies.LearningResourcesNonIctCosts,
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesNonIctCosts),
            Current = CurrentValues.LearningResourcesNonIctCosts,
            Custom = CustomInput.LearningResourcesNonIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel ITSection => new(
        "IT",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.EducationalIct.LearningResourcesIctCosts,
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesIctCosts),
            Current = CurrentValues.LearningResourcesIctCosts,
            Custom = CustomInput.LearningResourcesIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel NonEducationalSupportStaffSection => new(
        "Non-educational support staff",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.NonEducationalSupportStaff.AdministrativeClericalStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.AdministrativeClericalStaffCosts),
            Current = CurrentValues.AdministrativeClericalStaffCosts,
            Custom = CustomInput.AdministrativeClericalStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.NonEducationalSupportStaff.AuditorsCosts,
            Name = nameof(SchoolCustomDataViewModel.AuditorsCosts),
            Current = CurrentValues.AuditorsCosts,
            Custom = CustomInput.AuditorsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.NonEducationalSupportStaff.OtherStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherStaffCosts),
            Current = CurrentValues.OtherStaffCosts,
            Custom = CustomInput.OtherStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.NonEducationalSupportStaff.ProfessionalServicesNonCurriculumCosts,
            Name = nameof(SchoolCustomDataViewModel.ProfessionalServicesNonCurriculumCosts),
            Current = CurrentValues.ProfessionalServicesNonCurriculumCosts,
            Custom = CustomInput.ProfessionalServicesNonCurriculumCosts
        }
    );

    public SchoolCustomDataSectionViewModel PremisesAndServicesSection => new(
        "Premises and services",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.PremisesStaffServices.CleaningCaretakingCosts,
            Name = nameof(SchoolCustomDataViewModel.CleaningCaretakingCosts),
            Current = CurrentValues.CleaningCaretakingCosts,
            Custom = CustomInput.CleaningCaretakingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.PremisesStaffServices.MaintenancePremisesCosts,
            Name = nameof(SchoolCustomDataViewModel.MaintenancePremisesCosts),
            Current = CurrentValues.MaintenancePremisesCosts,
            Custom = CustomInput.MaintenancePremisesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.PremisesStaffServices.OtherOccupationCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherOccupationCosts),
            Current = CurrentValues.OtherOccupationCosts,
            Custom = CustomInput.OtherOccupationCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.PremisesStaffServices.PremisesStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.PremisesStaffCosts),
            Current = CurrentValues.PremisesStaffCosts,
            Custom = CustomInput.PremisesStaffCosts
        }
    );

    public SchoolCustomDataSectionViewModel TeachingAndTeachingSupportSection => new(
        "Teaching and teaching support",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.TeachingStaff.AgencySupplyTeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.AgencySupplyTeachingStaffCosts),
            Current = CurrentValues.AgencySupplyTeachingStaffCosts,
            Custom = CustomInput.AgencySupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.TeachingStaff.EducationSupportStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.EducationSupportStaffCosts),
            Current = CurrentValues.EducationSupportStaffCosts,
            Custom = CustomInput.EducationSupportStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.TeachingStaff.EducationalConsultancyCosts,
            Name = nameof(SchoolCustomDataViewModel.EducationalConsultancyCosts),
            Current = CurrentValues.EducationalConsultancyCosts,
            Custom = CustomInput.EducationalConsultancyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.TeachingStaff.SupplyTeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.SupplyTeachingStaffCosts),
            Current = CurrentValues.SupplyTeachingStaffCosts,
            Custom = CustomInput.SupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.TeachingStaff.TeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.TeachingStaffCosts),
            Current = CurrentValues.TeachingStaffCosts,
            Custom = CustomInput.TeachingStaffCosts
        });

    public SchoolCustomDataSectionViewModel UtilitiesSection => new(
        "Utilities",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Utilities.EnergyCosts,
            Name = nameof(SchoolCustomDataViewModel.EnergyCosts),
            Current = CurrentValues.EnergyCosts,
            Custom = CustomInput.EnergyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Utilities.WaterSewerageCosts,
            Name = nameof(SchoolCustomDataViewModel.WaterSewerageCosts),
            Current = CurrentValues.WaterSewerageCosts,
            Custom = CustomInput.WaterSewerageCosts
        });

    public SchoolCustomDataSectionViewModel OtherCostsSection => new(
        "Other costs",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.GroundsMaintenanceCosts,
            Name = nameof(SchoolCustomDataViewModel.GroundsMaintenanceCosts),
            Current = CurrentValues.GroundsMaintenanceCosts,
            Custom = CustomInput.GroundsMaintenanceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.IndirectEmployeeExpenses,
            Name = nameof(SchoolCustomDataViewModel.IndirectEmployeeExpenses),
            Current = CurrentValues.IndirectEmployeeExpenses,
            Custom = CustomInput.IndirectEmployeeExpenses
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.InterestChargesLoanBank,
            Name = nameof(SchoolCustomDataViewModel.InterestChargesLoanBank),
            Current = CurrentValues.InterestChargesLoanBank,
            Custom = CustomInput.InterestChargesLoanBank
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.OtherInsurancePremiumsCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherInsurancePremiumsCosts),
            Current = CurrentValues.OtherInsurancePremiumsCosts,
            Custom = CustomInput.OtherInsurancePremiumsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.PrivateFinanceInitiativeCharges,
            Name = nameof(SchoolCustomDataViewModel.PrivateFinanceInitiativeCharges),
            Current = CurrentValues.PrivateFinanceInitiativeCharges,
            Custom = CustomInput.PrivateFinanceInitiativeCharges
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.RentRatesCosts,
            Name = nameof(SchoolCustomDataViewModel.RentRatesCosts),
            Current = CurrentValues.RentRatesCosts,
            Custom = CustomInput.RentRatesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.SpecialFacilitiesCosts,
            Name = nameof(SchoolCustomDataViewModel.SpecialFacilitiesCosts),
            Current = CurrentValues.SpecialFacilitiesCosts,
            Custom = CustomInput.SpecialFacilitiesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.StaffDevelopmentTrainingCosts,
            Name = nameof(SchoolCustomDataViewModel.StaffDevelopmentTrainingCosts),
            Current = CurrentValues.StaffDevelopmentTrainingCosts,
            Custom = CustomInput.StaffDevelopmentTrainingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.StaffRelatedInsuranceCosts,
            Name = nameof(SchoolCustomDataViewModel.StaffRelatedInsuranceCosts),
            Current = CurrentValues.StaffRelatedInsuranceCosts,
            Custom = CustomInput.StaffRelatedInsuranceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SubCostCategories.Other.SupplyTeacherInsurableCosts,
            Name = nameof(SchoolCustomDataViewModel.SupplyTeacherInsurableCosts),
            Current = CurrentValues.SupplyTeacherInsurableCosts,
            Custom = CustomInput.SupplyTeacherInsurableCosts
        });

    public SchoolCustomDataSectionViewModel TotalsSection => new(
        "Totals",
        "Cost",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TotalIncome,
            Name = nameof(SchoolCustomDataViewModel.TotalIncome),
            Current = CurrentValues.TotalIncome,
            Custom = CustomInput.TotalIncome
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TotalExpenditure,
            Name = nameof(SchoolCustomDataViewModel.TotalExpenditure),
            Current = CurrentValues.TotalExpenditure,
            Custom = CustomInput.TotalExpenditure ?? CurrentValues.TotalExpenditure,
            ReadOnly = true
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.RevenueReserve,
            Name = nameof(SchoolCustomDataViewModel.RevenueReserve),
            Current = CurrentValues.RevenueReserve,
            Custom = CustomInput.RevenueReserve
        });

    public SchoolCustomDataSectionViewModel NonFinancialDataSection => new(
        "Non-financial figures",
        "Item",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.NumberOfPupilsFte,
            Name = nameof(SchoolCustomDataViewModel.NumberOfPupilsFte),
            Current = CurrentValues.NumberOfPupilsFte,
            Custom = CustomInput.NumberOfPupilsFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.FreeSchoolMealPercent,
            Name = nameof(SchoolCustomDataViewModel.FreeSchoolMealPercent),
            Current = CurrentValues.FreeSchoolMealPercent,
            Custom = CustomInput.FreeSchoolMealPercent,
            Units = SchoolCustomDataValueUnits.Percentage
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.SpecialEducationalNeedsPercent,
            Name = nameof(SchoolCustomDataViewModel.SpecialEducationalNeedsPercent),
            Current = CurrentValues.SpecialEducationalNeedsPercent,
            Custom = CustomInput.SpecialEducationalNeedsPercent,
            Units = SchoolCustomDataValueUnits.Percentage
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.FloorArea,
            Name = nameof(SchoolCustomDataViewModel.FloorArea),
            Current = CurrentValues.FloorArea,
            Custom = CustomInput.FloorArea,
            Units = SchoolCustomDataValueUnits.Area
        }
    );

    public SchoolCustomDataSectionViewModel WorkforceDataSection => new(
        "Workforce figures",
        "Item",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.WorkforceFte,
            Name = nameof(SchoolCustomDataViewModel.WorkforceFte),
            Current = CurrentValues.WorkforceFte,
            Custom = CustomInput.WorkforceFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TeachersFte,
            Name = nameof(SchoolCustomDataViewModel.TeachersFte),
            Current = CurrentValues.TeachersFte,
            Custom = CustomInput.TeachersFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.QualifiedTeacherPercent,
            Name = nameof(SchoolCustomDataViewModel.QualifiedTeacherPercent),
            Current = CurrentValues.QualifiedTeacherPercent,
            Custom = CustomInput.QualifiedTeacherPercent,
            Units = SchoolCustomDataValueUnits.Percentage
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.SeniorLeadershipFte,
            Name = nameof(SchoolCustomDataViewModel.SeniorLeadershipFte),
            Current = CurrentValues.SeniorLeadershipFte,
            Custom = CustomInput.SeniorLeadershipFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TeachingAssistantsFte,
            Name = nameof(SchoolCustomDataViewModel.TeachingAssistantsFte),
            Current = CurrentValues.TeachingAssistantsFte,
            Custom = CustomInput.TeachingAssistantsFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.NonClassroomSupportStaffFte,
            Name = nameof(SchoolCustomDataViewModel.NonClassroomSupportStaffFte),
            Current = CurrentValues.NonClassroomSupportStaffFte,
            Custom = CustomInput.NonClassroomSupportStaffFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.AuxiliaryStaffFte,
            Name = nameof(SchoolCustomDataViewModel.AuxiliaryStaffFte),
            Current = CurrentValues.AuxiliaryStaffFte,
            Custom = CustomInput.AuxiliaryStaffFte,
            Units = SchoolCustomDataValueUnits.Actual
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.WorkforceHeadcount,
            Name = nameof(SchoolCustomDataViewModel.WorkforceHeadcount),
            Current = CurrentValues.WorkforceHeadcount,
            Custom = CustomInput.WorkforceHeadcount,
            Units = SchoolCustomDataValueUnits.Actual
        }
    );
}

public record SchoolCustomDataSectionViewModel
{
    public SchoolCustomDataSectionViewModel(string title, string metricHeading, params SchoolCustomDataValueViewModel[] values)
    {
        Title = title;
        MetricHeading = metricHeading;
        Values = values;
    }

    public string Title { get; init; }
    public string MetricHeading { get; init; }
    public IEnumerable<SchoolCustomDataValueViewModel> Values { get; init; }
}

public record SchoolCustomDataValueViewModel
{
    public string Title { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal? Current { get; init; }
    public decimal? Custom { get; init; }
    public bool ReadOnly { get; init; }
    public bool Hidden { get; init; }
    public SchoolCustomDataValueUnits Units { get; init; } = SchoolCustomDataValueUnits.Currency;
}

public enum SchoolCustomDataValueUnits
{
    Actual,
    Area,
    Currency,
    Percentage
}