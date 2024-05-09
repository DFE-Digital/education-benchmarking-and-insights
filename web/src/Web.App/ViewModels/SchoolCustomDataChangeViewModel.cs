using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCustomDataChangeViewModel(
    School school
)
{
    public SchoolCustomDataChangeViewModel(
        School school,
        Finances finances,
        Income income,
        SchoolExpenditure expenditure,
        Census census,
        FloorAreaMetric floorArea) : this(school)
    {
        CurrentValues = new SchoolCustomDataViewModel
        {
            // Administrative supplies
            AdministrativeSuppliesCosts = expenditure.AdministrativeSuppliesCosts,

            // Catering
            CateringStaffCosts = expenditure.CateringStaffCosts,
            CateringSupplies = expenditure.CateringSuppliesCosts,
            CateringIncome = income.IncomeCatering,

            // Educational supplies
            ExaminationFeesCosts = expenditure.ExaminationFeesCosts,
            LearningResourcesNonIctCosts = expenditure.LearningResourcesNonIctCosts,

            // IT
            LearningResourcesIctCosts = expenditure.LearningResourcesIctCosts,

            // Non-educational support staff
            AdministrativeClericalStaffCosts = expenditure.AdministrativeClericalStaffCosts,
            AuditorsCosts = expenditure.AuditorsCosts,
            OtherStaffCosts = expenditure.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = expenditure.ProfessionalServicesNonCurriculumCosts,

            // Premises and services
            CleaningCaretakingCosts = expenditure.CleaningCaretakingCosts,
            MaintenancePremisesCosts = expenditure.MaintenancePremisesCosts,
            OtherOccupationCosts = expenditure.OtherOccupationCosts,
            PremisesStaffCosts = expenditure.PremisesStaffCosts,

            // Teaching and teaching support
            AgencySupplyTeachingStaffCosts = expenditure.AgencySupplyTeachingStaffCosts,
            EducationSupportStaffCosts = expenditure.EducationSupportStaffCosts,
            EducationalConsultancyCosts = expenditure.EducationalConsultancyCosts,
            SupplyTeachingStaffCosts = expenditure.SupplyTeachingStaffCosts,
            TeachingStaffCosts = expenditure.TeachingStaffCosts,

            // Utilities
            EnergyCosts = expenditure.EnergyCosts,
            WaterSewerageCosts = expenditure.WaterSewerageCosts,

            // Other costs
            DirectRevenueFinancingCosts = expenditure.DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts = expenditure.GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = expenditure.IndirectEmployeeExpenses,
            InterestChargesLoanBank = expenditure.InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = expenditure.OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = expenditure.PrivateFinanceInitiativeCharges,
            RentRatesCosts = expenditure.RentRatesCosts,
            SpecialFacilitiesCosts = expenditure.SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = expenditure.StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = expenditure.StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = expenditure.SupplyTeacherInsurableCosts,

            // Totals
            TotalIncome = finances.TotalIncome,
            TotalExpenditure = finances.TotalExpenditure,
            RevenueReserve = finances.RevenueReserve,

            // Non-financial data
            TotalNumberOfTeachersFte = finances.TotalNumberOfTeachersFte,
            FreeSchoolMealPercent = finances.FreeSchoolMealPercent,
            SpecialEducationalNeedsPercent = finances.SpecialEducationalNeedsPercent,
            FloorArea = floorArea.FloorArea,

            // Workforce data
            WorkforceFte = census.WorkforceFte,
            TeachersFte = census.TeachersFte,
            SeniorLeadershipFte = census.SeniorLeadershipFte
        };
    }

    public School School { get; } = school;

    public SchoolCustomDataViewModel CurrentValues { get; } = new();
    public SchoolCustomDataViewModel? CustomInput { get; }

    public SchoolCustomDataSectionViewModel AdministrativeSuppliesSection => new(
        "Administrative supplies",
        new SchoolCustomDataValueViewModel
        {
            Title = "Administrative supplies (non-educational)",
            Name = nameof(SchoolCustomDataViewModel.AdministrativeSuppliesCosts),
            Current = CurrentValues.AdministrativeSuppliesCosts,
            Custom = CustomInput?.AdministrativeSuppliesCosts
        }
    );

    public SchoolCustomDataSectionViewModel CateringSection => new(
        "Catering",
        new SchoolCustomDataValueViewModel
        {
            Title = "Catering staff",
            Name = nameof(SchoolCustomDataViewModel.CateringStaffCosts),
            Current = CurrentValues.CateringStaffCosts,
            Custom = CustomInput?.CateringStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Catering supplies",
            Name = nameof(SchoolCustomDataViewModel.CateringSupplies),
            Current = CurrentValues.CateringSupplies,
            Custom = CustomInput?.CateringSupplies
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Income from catering",
            Name = nameof(SchoolCustomDataViewModel.CateringIncome),
            Current = CurrentValues.CateringIncome,
            Custom = CustomInput?.CateringIncome
        }
    );

    public SchoolCustomDataSectionViewModel EducationalSuppliesSection => new(
        "Educational supplies",
        new SchoolCustomDataValueViewModel
        {
            Title = "Examination fees",
            Name = nameof(SchoolCustomDataViewModel.ExaminationFeesCosts),
            Current = CurrentValues.ExaminationFeesCosts,
            Custom = CustomInput?.ExaminationFeesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Learning resources (not ICT equipment)",
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesNonIctCosts),
            Current = CurrentValues.LearningResourcesNonIctCosts,
            Custom = CustomInput?.LearningResourcesNonIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel ITSection => new(
        "IT",
        new SchoolCustomDataValueViewModel
        {
            Title = "ICT learning resources",
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesIctCosts),
            Current = CurrentValues.LearningResourcesIctCosts,
            Custom = CustomInput?.LearningResourcesIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel NonEducationalSupportStaffSection => new(
        "Non-educational support staff",
        new SchoolCustomDataValueViewModel
        {
            Title = "Administrative and clerical staff",
            Name = nameof(SchoolCustomDataViewModel.AdministrativeClericalStaffCosts),
            Current = CurrentValues.AdministrativeClericalStaffCosts,
            Custom = CustomInput?.AdministrativeClericalStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Auditor costs",
            Name = nameof(SchoolCustomDataViewModel.AuditorsCosts),
            Current = CurrentValues.AuditorsCosts,
            Custom = CustomInput?.AuditorsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Other staff",
            Name = nameof(SchoolCustomDataViewModel.OtherStaffCosts),
            Current = CurrentValues.OtherStaffCosts,
            Custom = CustomInput?.OtherStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Professional services (non-curriculum)",
            Name = nameof(SchoolCustomDataViewModel.ProfessionalServicesNonCurriculumCosts),
            Current = CurrentValues.ProfessionalServicesNonCurriculumCosts,
            Custom = CustomInput?.ProfessionalServicesNonCurriculumCosts
        }
    );

    public SchoolCustomDataSectionViewModel PremisesAndServicesSection => new(
        "Premises and services",
        new SchoolCustomDataValueViewModel
        {
            Title = "Cleaning and caretaking",
            Name = nameof(SchoolCustomDataViewModel.CleaningCaretakingCosts),
            Current = CurrentValues.CleaningCaretakingCosts,
            Custom = CustomInput?.CleaningCaretakingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Maintenance of premises",
            Name = nameof(SchoolCustomDataViewModel.MaintenancePremisesCosts),
            Current = CurrentValues.MaintenancePremisesCosts,
            Custom = CustomInput?.MaintenancePremisesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Other occupation costs",
            Name = nameof(SchoolCustomDataViewModel.OtherOccupationCosts),
            Current = CurrentValues.OtherOccupationCosts,
            Custom = CustomInput?.OtherOccupationCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Premises staff",
            Name = nameof(SchoolCustomDataViewModel.PremisesStaffCosts),
            Current = CurrentValues.PremisesStaffCosts,
            Custom = CustomInput?.PremisesStaffCosts
        }
    );

    public SchoolCustomDataSectionViewModel TeachingAndTeachingSupportSection => new(
        "Teaching and teaching support",
        new SchoolCustomDataValueViewModel
        {
            Title = "Agency supply teaching staff",
            Name = nameof(SchoolCustomDataViewModel.AgencySupplyTeachingStaffCosts),
            Current = CurrentValues.AgencySupplyTeachingStaffCosts,
            Custom = CustomInput?.AgencySupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Education support staff",
            Name = nameof(SchoolCustomDataViewModel.EducationSupportStaffCosts),
            Current = CurrentValues.EducationSupportStaffCosts,
            Custom = CustomInput?.EducationSupportStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Educational consultancy",
            Name = nameof(SchoolCustomDataViewModel.EducationalConsultancyCosts),
            Current = CurrentValues.EducationalConsultancyCosts,
            Custom = CustomInput?.EducationalConsultancyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Supply teaching staff",
            Name = nameof(SchoolCustomDataViewModel.SupplyTeachingStaffCosts),
            Current = CurrentValues.SupplyTeachingStaffCosts,
            Custom = CustomInput?.SupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Teaching staff",
            Name = nameof(SchoolCustomDataViewModel.TeachingStaffCosts),
            Current = CurrentValues.TeachingStaffCosts,
            Custom = CustomInput?.TeachingStaffCosts
        });

    public SchoolCustomDataSectionViewModel UtilitiesSection => new(
        "Utilities",
        new SchoolCustomDataValueViewModel
        {
            Title = "Energy",
            Name = nameof(SchoolCustomDataViewModel.EnergyCosts),
            Current = CurrentValues.EnergyCosts,
            Custom = CustomInput?.EnergyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Water and sewerage",
            Name = nameof(SchoolCustomDataViewModel.WaterSewerageCosts),
            Current = CurrentValues.WaterSewerageCosts,
            Custom = CustomInput?.WaterSewerageCosts
        });

    public SchoolCustomDataSectionViewModel OtherCostsSection => new(
        "Other costs",
        new SchoolCustomDataValueViewModel
        {
            Title = "Administrative and clerical staff",
            Name = nameof(SchoolCustomDataViewModel.AdministrativeClericalStaffCosts),
            Current = CurrentValues.AdministrativeClericalStaffCosts,
            Custom = CustomInput?.AdministrativeClericalStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Direct revenue financing",
            Name = nameof(SchoolCustomDataViewModel.DirectRevenueFinancingCosts),
            Current = CurrentValues.DirectRevenueFinancingCosts,
            Custom = CustomInput?.DirectRevenueFinancingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Grounds maintenance",
            Name = nameof(SchoolCustomDataViewModel.GroundsMaintenanceCosts),
            Current = CurrentValues.GroundsMaintenanceCosts,
            Custom = CustomInput?.GroundsMaintenanceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Indirect employee expenses",
            Name = nameof(SchoolCustomDataViewModel.IndirectEmployeeExpenses),
            Current = CurrentValues.IndirectEmployeeExpenses,
            Custom = CustomInput?.IndirectEmployeeExpenses
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Interest charges for loan and bank",
            Name = nameof(SchoolCustomDataViewModel.InterestChargesLoanBank),
            Current = CurrentValues.InterestChargesLoanBank,
            Custom = CustomInput?.InterestChargesLoanBank
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Other insurance premiums",
            Name = nameof(SchoolCustomDataViewModel.OtherInsurancePremiumsCosts),
            Current = CurrentValues.OtherInsurancePremiumsCosts,
            Custom = CustomInput?.OtherInsurancePremiumsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Private Finance Initiative (PFI) charges",
            Name = nameof(SchoolCustomDataViewModel.PrivateFinanceInitiativeCharges),
            Current = CurrentValues.PrivateFinanceInitiativeCharges,
            Custom = CustomInput?.PrivateFinanceInitiativeCharges
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Rent and rates",
            Name = nameof(SchoolCustomDataViewModel.RentRatesCosts),
            Current = CurrentValues.RentRatesCosts,
            Custom = CustomInput?.RentRatesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Special facilities",
            Name = nameof(SchoolCustomDataViewModel.SpecialFacilitiesCosts),
            Current = CurrentValues.SpecialFacilitiesCosts,
            Custom = CustomInput?.SpecialFacilitiesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Staff development and training",
            Name = nameof(SchoolCustomDataViewModel.StaffDevelopmentTrainingCosts),
            Current = CurrentValues.StaffDevelopmentTrainingCosts,
            Custom = CustomInput?.StaffDevelopmentTrainingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Staff-related insurance",
            Name = nameof(SchoolCustomDataViewModel.StaffRelatedInsuranceCosts),
            Current = CurrentValues.StaffRelatedInsuranceCosts,
            Custom = CustomInput?.StaffRelatedInsuranceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Supply teacher insurance",
            Name = nameof(SchoolCustomDataViewModel.SupplyTeacherInsurableCosts),
            Current = CurrentValues.SupplyTeacherInsurableCosts,
            Custom = CustomInput?.SupplyTeacherInsurableCosts
        });

    public SchoolCustomDataSectionViewModel TotalsSection => new(
        "Totals",
        new SchoolCustomDataValueViewModel
        {
            Title = "Total income",
            Name = nameof(SchoolCustomDataViewModel.TotalIncome),
            Current = CurrentValues.TotalIncome,
            Custom = CustomInput?.TotalIncome,
            ReadOnly = true
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Total spending",
            Name = nameof(SchoolCustomDataViewModel.TotalExpenditure),
            Current = CurrentValues.TotalExpenditure,
            Custom = CustomInput?.TotalExpenditure,
            ReadOnly = true
        },
        new SchoolCustomDataValueViewModel
        {
            Title = "Revenue reserve",
            Name = nameof(SchoolCustomDataViewModel.RevenueReserve),
            Current = CurrentValues.RevenueReserve,
            Custom = CustomInput?.RevenueReserve,
            ReadOnly = true
        });
}

public record SchoolCustomDataSectionViewModel
{
    public SchoolCustomDataSectionViewModel(string title, params SchoolCustomDataValueViewModel[] values)
    {
        Title = title;
        Values = values;
    }

    public string Title { get; init; }
    public IEnumerable<SchoolCustomDataValueViewModel> Values { get; init; }
}

public record SchoolCustomDataValueViewModel
{
    public string Title { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal? Current { get; init; }
    public decimal? Custom { get; init; }
    public bool ReadOnly { get; init; }
}

public record SchoolCustomDataViewModel
{
    // Administrative supplies
    public decimal? AdministrativeSuppliesCosts { get; init; }

    // Catering
    public decimal? CateringStaffCosts { get; init; }
    public decimal? CateringSupplies { get; init; }
    public decimal? CateringIncome { get; init; }

    // Educational supplies
    public decimal? ExaminationFeesCosts { get; init; }
    public decimal? LearningResourcesNonIctCosts { get; init; }

    // IT
    public decimal? LearningResourcesIctCosts { get; init; }

    // Non-educational support staff
    public decimal? AdministrativeClericalStaffCosts { get; init; }
    public decimal? AuditorsCosts { get; init; }
    public decimal? OtherStaffCosts { get; init; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; init; }

    // Premises and services
    public decimal? CleaningCaretakingCosts { get; init; }
    public decimal? MaintenancePremisesCosts { get; init; }
    public decimal? OtherOccupationCosts { get; init; }
    public decimal? PremisesStaffCosts { get; init; }

    // Teaching and teaching support
    public decimal? AgencySupplyTeachingStaffCosts { get; init; }
    public decimal? EducationSupportStaffCosts { get; init; }
    public decimal? EducationalConsultancyCosts { get; init; }
    public decimal? SupplyTeachingStaffCosts { get; init; }
    public decimal? TeachingStaffCosts { get; init; }

    // Utilities
    public decimal? EnergyCosts { get; init; }
    public decimal? WaterSewerageCosts { get; init; }

    // Other costs
    public decimal? DirectRevenueFinancingCosts { get; init; }
    public decimal? GroundsMaintenanceCosts { get; init; }
    public decimal? IndirectEmployeeExpenses { get; init; }
    public decimal? InterestChargesLoanBank { get; init; }
    public decimal? OtherInsurancePremiumsCosts { get; init; }
    public decimal? PrivateFinanceInitiativeCharges { get; init; }
    public decimal? RentRatesCosts { get; init; }
    public decimal? SpecialFacilitiesCosts { get; init; }
    public decimal? StaffDevelopmentTrainingCosts { get; init; }
    public decimal? StaffRelatedInsuranceCosts { get; init; }
    public decimal? SupplyTeacherInsurableCosts { get; init; }

    // Totals
    public decimal TotalIncome { get; init; }
    public decimal TotalExpenditure { get; init; }
    public decimal RevenueReserve { get; init; }

    // Non-financial data
    public decimal? TotalNumberOfTeachersFte { get; init; }
    public decimal? FreeSchoolMealPercent { get; init; }
    public decimal? SpecialEducationalNeedsPercent { get; init; }
    public int? FloorArea { get; init; }

    // Workforce data
    public decimal? WorkforceFte { get; init; }
    public decimal? TeachersFte { get; init; }
    public decimal? SeniorLeadershipFte { get; init; }
}