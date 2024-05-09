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
        FloorAreaMetric floorArea,
        CustomData? customData) : this(school)
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

        if (customData != null)
        {
            CustomInput = new SchoolCustomDataViewModel
            {
                // Administrative supplies
                AdministrativeSuppliesCosts = customData.AdministrativeSuppliesCosts,

                // Catering
                CateringStaffCosts = customData.CateringStaffCosts,
                CateringSupplies = customData.CateringSupplies,
                CateringIncome = customData.CateringIncome,

                // Educational supplies
                ExaminationFeesCosts = customData.ExaminationFeesCosts,
                LearningResourcesNonIctCosts = customData.LearningResourcesNonIctCosts,

                // IT
                LearningResourcesIctCosts = customData.LearningResourcesIctCosts,

                // Non-educational support staff
                AdministrativeClericalStaffCosts = customData.AdministrativeClericalStaffCosts,
                AuditorsCosts = customData.AuditorsCosts,
                OtherStaffCosts = customData.OtherStaffCosts,
                ProfessionalServicesNonCurriculumCosts = customData.ProfessionalServicesNonCurriculumCosts,

                // Premises and services
                CleaningCaretakingCosts = customData.CleaningCaretakingCosts,
                MaintenancePremisesCosts = customData.MaintenancePremisesCosts,
                OtherOccupationCosts = customData.OtherOccupationCosts,
                PremisesStaffCosts = customData.PremisesStaffCosts,

                // Teaching and teaching support
                AgencySupplyTeachingStaffCosts = customData.AgencySupplyTeachingStaffCosts,
                EducationSupportStaffCosts = customData.EducationSupportStaffCosts,
                EducationalConsultancyCosts = customData.EducationalConsultancyCosts,
                SupplyTeachingStaffCosts = customData.SupplyTeachingStaffCosts,
                TeachingStaffCosts = customData.TeachingStaffCosts,

                // Utilities
                EnergyCosts = customData.EnergyCosts,
                WaterSewerageCosts = customData.WaterSewerageCosts,

                // Other costs
                DirectRevenueFinancingCosts = customData.DirectRevenueFinancingCosts,
                GroundsMaintenanceCosts = customData.GroundsMaintenanceCosts,
                IndirectEmployeeExpenses = customData.IndirectEmployeeExpenses,
                InterestChargesLoanBank = customData.InterestChargesLoanBank,
                OtherInsurancePremiumsCosts = customData.OtherInsurancePremiumsCosts,
                PrivateFinanceInitiativeCharges = customData.PrivateFinanceInitiativeCharges,
                RentRatesCosts = customData.RentRatesCosts,
                SpecialFacilitiesCosts = customData.SpecialFacilitiesCosts,
                StaffDevelopmentTrainingCosts = customData.StaffDevelopmentTrainingCosts,
                StaffRelatedInsuranceCosts = customData.StaffRelatedInsuranceCosts,
                SupplyTeacherInsurableCosts = customData.SupplyTeacherInsurableCosts,

                // Totals
                TotalIncome = customData.TotalIncome,
                TotalExpenditure = customData.TotalExpenditure,
                RevenueReserve = customData.RevenueReserve,

                // Non-financial data
                TotalNumberOfTeachersFte = customData.TotalNumberOfTeachersFte,
                FreeSchoolMealPercent = customData.FreeSchoolMealPercent,
                SpecialEducationalNeedsPercent = customData.SpecialEducationalNeedsPercent,
                FloorArea = customData.FloorArea,

                // Workforce data
                WorkforceFte = customData.WorkforceFte,
                TeachersFte = customData.TeachersFte,
                SeniorLeadershipFte = customData.SeniorLeadershipFte
            };
        }
    }

    public School School { get; } = school;

    public SchoolCustomDataViewModel CurrentValues { get; } = new();
    public SchoolCustomDataViewModel CustomInput { get; } = new();

    public SchoolCustomDataSectionViewModel AdministrativeSuppliesSection => new(
        "Administrative supplies",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.AdministrativeSuppliesCosts,
            Name = nameof(SchoolCustomDataViewModel.AdministrativeSuppliesCosts),
            Current = CurrentValues.AdministrativeSuppliesCosts,
            Custom = CustomInput.AdministrativeSuppliesCosts
        }
    );

    public SchoolCustomDataSectionViewModel CateringSection => new(
        "Catering",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.CateringStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.CateringStaffCosts),
            Current = CurrentValues.CateringStaffCosts,
            Custom = CustomInput.CateringStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.CateringSupplies,
            Name = nameof(SchoolCustomDataViewModel.CateringSupplies),
            Current = CurrentValues.CateringSupplies,
            Custom = CustomInput.CateringSupplies
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.CateringIncome,
            Name = nameof(SchoolCustomDataViewModel.CateringIncome),
            Current = CurrentValues.CateringIncome,
            Custom = CustomInput.CateringIncome
        }
    );

    public SchoolCustomDataSectionViewModel EducationalSuppliesSection => new(
        "Educational supplies",
        new SchoolCustomDataValueViewModel
        {
            Title =SchoolCustomDataViewModelTitles.ExaminationFeesCosts,
            Name = nameof(SchoolCustomDataViewModel.ExaminationFeesCosts),
            Current = CurrentValues.ExaminationFeesCosts,
            Custom = CustomInput.ExaminationFeesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title =SchoolCustomDataViewModelTitles.LearningResourcesNonIctCosts,
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesNonIctCosts),
            Current = CurrentValues.LearningResourcesNonIctCosts,
            Custom = CustomInput.LearningResourcesNonIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel ITSection => new(
        "IT",
        new SchoolCustomDataValueViewModel
        {
            Title =SchoolCustomDataViewModelTitles.LearningResourcesIctCosts,
            Name = nameof(SchoolCustomDataViewModel.LearningResourcesIctCosts),
            Current = CurrentValues.LearningResourcesIctCosts,
            Custom = CustomInput.LearningResourcesIctCosts
        }
    );

    public SchoolCustomDataSectionViewModel NonEducationalSupportStaffSection => new(
        "Non-educational support staff",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.AdministrativeClericalStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.AdministrativeClericalStaffCosts),
            Current = CurrentValues.AdministrativeClericalStaffCosts,
            Custom = CustomInput.AdministrativeClericalStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.AuditorsCosts,
            Name = nameof(SchoolCustomDataViewModel.AuditorsCosts),
            Current = CurrentValues.AuditorsCosts,
            Custom = CustomInput.AuditorsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.OtherStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherStaffCosts),
            Current = CurrentValues.OtherStaffCosts,
            Custom = CustomInput.OtherStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.ProfessionalServicesNonCurriculumCosts,
            Name = nameof(SchoolCustomDataViewModel.ProfessionalServicesNonCurriculumCosts),
            Current = CurrentValues.ProfessionalServicesNonCurriculumCosts,
            Custom = CustomInput.ProfessionalServicesNonCurriculumCosts
        }
    );

    public SchoolCustomDataSectionViewModel PremisesAndServicesSection => new(
        "Premises and services",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.CleaningCaretakingCosts,
            Name = nameof(SchoolCustomDataViewModel.CleaningCaretakingCosts),
            Current = CurrentValues.CleaningCaretakingCosts,
            Custom = CustomInput.CleaningCaretakingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.MaintenancePremisesCosts,
            Name = nameof(SchoolCustomDataViewModel.MaintenancePremisesCosts),
            Current = CurrentValues.MaintenancePremisesCosts,
            Custom = CustomInput.MaintenancePremisesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.OtherOccupationCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherOccupationCosts),
            Current = CurrentValues.OtherOccupationCosts,
            Custom = CustomInput.OtherOccupationCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.PremisesStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.PremisesStaffCosts),
            Current = CurrentValues.PremisesStaffCosts,
            Custom = CustomInput.PremisesStaffCosts
        }
    );

    public SchoolCustomDataSectionViewModel TeachingAndTeachingSupportSection => new(
        "Teaching and teaching support",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.AgencySupplyTeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.AgencySupplyTeachingStaffCosts),
            Current = CurrentValues.AgencySupplyTeachingStaffCosts,
            Custom = CustomInput.AgencySupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.EducationSupportStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.EducationSupportStaffCosts),
            Current = CurrentValues.EducationSupportStaffCosts,
            Custom = CustomInput.EducationSupportStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title =SchoolCustomDataViewModelTitles.EducationalConsultancyCosts,
            Name = nameof(SchoolCustomDataViewModel.EducationalConsultancyCosts),
            Current = CurrentValues.EducationalConsultancyCosts,
            Custom = CustomInput.EducationalConsultancyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.SupplyTeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.SupplyTeachingStaffCosts),
            Current = CurrentValues.SupplyTeachingStaffCosts,
            Custom = CustomInput.SupplyTeachingStaffCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TeachingStaffCosts,
            Name = nameof(SchoolCustomDataViewModel.TeachingStaffCosts),
            Current = CurrentValues.TeachingStaffCosts,
            Custom = CustomInput.TeachingStaffCosts
        });

    public SchoolCustomDataSectionViewModel UtilitiesSection => new(
        "Utilities",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.EnergyCosts,
            Name = nameof(SchoolCustomDataViewModel.EnergyCosts),
            Current = CurrentValues.EnergyCosts,
            Custom = CustomInput.EnergyCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.WaterSewerageCosts,
            Name = nameof(SchoolCustomDataViewModel.WaterSewerageCosts),
            Current = CurrentValues.WaterSewerageCosts,
            Custom = CustomInput.WaterSewerageCosts
        });

    public SchoolCustomDataSectionViewModel OtherCostsSection => new(
        "Other costs",
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.DirectRevenueFinancingCosts,
            Name = nameof(SchoolCustomDataViewModel.DirectRevenueFinancingCosts),
            Current = CurrentValues.DirectRevenueFinancingCosts,
            Custom = CustomInput.DirectRevenueFinancingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.GroundsMaintenanceCosts,
            Name = nameof(SchoolCustomDataViewModel.GroundsMaintenanceCosts),
            Current = CurrentValues.GroundsMaintenanceCosts,
            Custom = CustomInput.GroundsMaintenanceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.IndirectEmployeeExpenses,
            Name = nameof(SchoolCustomDataViewModel.IndirectEmployeeExpenses),
            Current = CurrentValues.IndirectEmployeeExpenses,
            Custom = CustomInput.IndirectEmployeeExpenses
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.InterestChargesLoanBank,
            Name = nameof(SchoolCustomDataViewModel.InterestChargesLoanBank),
            Current = CurrentValues.InterestChargesLoanBank,
            Custom = CustomInput.InterestChargesLoanBank
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.OtherInsurancePremiumsCosts,
            Name = nameof(SchoolCustomDataViewModel.OtherInsurancePremiumsCosts),
            Current = CurrentValues.OtherInsurancePremiumsCosts,
            Custom = CustomInput.OtherInsurancePremiumsCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.PrivateFinanceInitiativeCharges,
            Name = nameof(SchoolCustomDataViewModel.PrivateFinanceInitiativeCharges),
            Current = CurrentValues.PrivateFinanceInitiativeCharges,
            Custom = CustomInput.PrivateFinanceInitiativeCharges
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.RentRatesCosts,
            Name = nameof(SchoolCustomDataViewModel.RentRatesCosts),
            Current = CurrentValues.RentRatesCosts,
            Custom = CustomInput.RentRatesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.SpecialFacilitiesCosts,
            Name = nameof(SchoolCustomDataViewModel.SpecialFacilitiesCosts),
            Current = CurrentValues.SpecialFacilitiesCosts,
            Custom = CustomInput.SpecialFacilitiesCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.StaffDevelopmentTrainingCosts,
            Name = nameof(SchoolCustomDataViewModel.StaffDevelopmentTrainingCosts),
            Current = CurrentValues.StaffDevelopmentTrainingCosts,
            Custom = CustomInput.StaffDevelopmentTrainingCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.StaffRelatedInsuranceCosts,
            Name = nameof(SchoolCustomDataViewModel.StaffRelatedInsuranceCosts),
            Current = CurrentValues.StaffRelatedInsuranceCosts,
            Custom = CustomInput.StaffRelatedInsuranceCosts
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.SupplyTeacherInsurableCosts,
            Name = nameof(SchoolCustomDataViewModel.SupplyTeacherInsurableCosts),
            Current = CurrentValues.SupplyTeacherInsurableCosts,
            Custom = CustomInput.SupplyTeacherInsurableCosts
        });

    public SchoolCustomDataSectionViewModel TotalsSection => new(
        "Totals",
        new SchoolCustomDataValueViewModel
        {
            Title =SchoolCustomDataViewModelTitles.TotalIncome,
            Name = nameof(SchoolCustomDataViewModel.TotalIncome),
            Current = CurrentValues.TotalIncome,
            Custom = CustomInput.TotalIncome,
            ReadOnly = true
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.TotalExpenditure,
            Name = nameof(SchoolCustomDataViewModel.TotalExpenditure),
            Current = CurrentValues.TotalExpenditure,
            Custom = CustomInput.TotalExpenditure,
            ReadOnly = true
        },
        new SchoolCustomDataValueViewModel
        {
            Title = SchoolCustomDataViewModelTitles.RevenueReserve,
            Name = nameof(SchoolCustomDataViewModel.RevenueReserve),
            Current = CurrentValues.RevenueReserve,
            Custom = CustomInput.RevenueReserve,
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