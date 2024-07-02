using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataFinancialData : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBalance _balance;
    private readonly Census _census;
    private readonly SchoolExpenditure _customExpenditure;
    private readonly SchoolIncome _customIncome;
    private readonly SchoolExpenditure _expenditure;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly SchoolIncome _income;

    public WhenViewingCustomDataFinancialData(SchoolBenchmarkingWebAppClient client) : base(client)
    {

        _income = Fixture.Build<SchoolIncome>()
            .Create();

        _customIncome = Fixture.Build<SchoolIncome>()
            .Create();

        _expenditure = Fixture.Build<SchoolExpenditure>()
            .Create();

        _customExpenditure = Fixture.Build<SchoolExpenditure>()
            .Create();

        _census = Fixture.Build<Census>()
            .Create();

        _balance = Fixture.Build<SchoolBalance>()
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(FinancialDataCustomDataViewModel.AdministrativeSuppliesCosts), _customExpenditure.AdministrativeSuppliesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.CateringStaffCosts), _customExpenditure.CateringStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.CateringSuppliesCosts), _customExpenditure.CateringSuppliesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.ExaminationFeesCosts), _customExpenditure.ExaminationFeesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.LearningResourcesNonIctCosts), _customExpenditure.LearningResourcesNonIctCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.LearningResourcesIctCosts), _customExpenditure.LearningResourcesIctCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.AdministrativeClericalStaffCosts), _customExpenditure.AdministrativeClericalStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.AuditorsCosts), _customExpenditure.AuditorsCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.OtherStaffCosts), _customExpenditure.OtherStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.ProfessionalServicesNonCurriculumCosts), _customExpenditure.ProfessionalServicesNonCurriculumCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.CleaningCaretakingCosts), _customExpenditure.CleaningCaretakingCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.MaintenancePremisesCosts), _customExpenditure.MaintenancePremisesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.OtherOccupationCosts), _customExpenditure.OtherOccupationCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.PremisesStaffCosts), _customExpenditure.PremisesStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.AgencySupplyTeachingStaffCosts), _customExpenditure.AgencySupplyTeachingStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.EducationSupportStaffCosts), _customExpenditure.EducationSupportStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.EducationalConsultancyCosts), _customExpenditure.EducationalConsultancyCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.SupplyTeachingStaffCosts), _customExpenditure.SupplyTeachingStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.TeachingStaffCosts), _customExpenditure.TeachingStaffCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.EnergyCosts), _customExpenditure.EnergyCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.WaterSewerageCosts), _customExpenditure.WaterSewerageCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.DirectRevenueFinancingCosts), _customExpenditure.DirectRevenueFinancingCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.GroundsMaintenanceCosts), _customExpenditure.GroundsMaintenanceCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.IndirectEmployeeExpenses), _customExpenditure.IndirectEmployeeExpenses
            },
            {
                nameof(FinancialDataCustomDataViewModel.InterestChargesLoanBank), _customExpenditure.InterestChargesLoanBank
            },
            {
                nameof(FinancialDataCustomDataViewModel.OtherInsurancePremiumsCosts), _customExpenditure.OtherInsurancePremiumsCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.PrivateFinanceInitiativeCharges), _customExpenditure.PrivateFinanceInitiativeCharges
            },
            {
                nameof(FinancialDataCustomDataViewModel.RentRatesCosts), _customExpenditure.RentRatesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.SpecialFacilitiesCosts), _customExpenditure.SpecialFacilitiesCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.StaffDevelopmentTrainingCosts), _customExpenditure.StaffDevelopmentTrainingCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.StaffRelatedInsuranceCosts), _customExpenditure.StaffRelatedInsuranceCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.SupplyTeacherInsurableCosts), _customExpenditure.SupplyTeacherInsurableCosts
            },
            {
                nameof(FinancialDataCustomDataViewModel.TotalIncome), _customIncome.TotalIncome
            },
            {
                nameof(FinancialDataCustomDataViewModel.TotalExpenditure), _customExpenditure.TotalExpenditure
            },
            {
                nameof(FinancialDataCustomDataViewModel.RevenueReserve), _balance.RevenueReserve
            }
        };
    }

    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanSubmitValidCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty));
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanSubmitEmptyCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task SetsAndGetsValidCustomValuesToSession()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty));
        });

        // go forward...
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        // ... and then back again
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.URN).ToAbsolute());
        var customValues = page.QuerySelectorAll("input").Not("[type='hidden']").ToList();
        Assert.Equal(34, customValues.Count);

        foreach (var customValue in customValues)
        {
            var actual = (customValue as IHtmlInputElement)?.Value;
            var field = customValue.Id ?? string.Empty;
            var expected = field switch
            {
                nameof(FinancialDataCustomDataViewModel.TotalIncome) => _customIncome.TotalIncome,
                _ => _customExpenditure.GetType().GetProperty(field)?.GetValue(_customExpenditure)
            };

            if (expected != null && decimal.TryParse(expected.ToString(), out var parsed))
            {
                expected = parsed.ToSimpleDisplay();
            }

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }

    [Fact]
    public async Task ShowsErrorOnInvalidValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, _ => "invalid"));
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.URN).ToAbsolute());
        DocumentAssert.SummaryErrors(
            page,
            (nameof(FinancialDataCustomDataViewModel.AdministrativeSuppliesCosts), "Enter administrative supplies (non-educational) in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CateringStaffCosts), "Enter catering staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CateringSuppliesCosts), "Enter catering supplies in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.ExaminationFeesCosts), "Enter examination fees in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.LearningResourcesNonIctCosts), "Enter learning resources (not ICT equipment) in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.LearningResourcesIctCosts), "Enter ICT learning resources in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.AdministrativeClericalStaffCosts), "Enter administrative and clerical staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.AuditorsCosts), "Enter auditor costs in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.OtherStaffCosts), "Enter other staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.ProfessionalServicesNonCurriculumCosts), "Enter professional services (non-curriculum) in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CleaningCaretakingCosts), "Enter cleaning and caretaking in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.MaintenancePremisesCosts), "Enter maintenance of premises in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.OtherOccupationCosts), "Enter other occupation costs in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.PremisesStaffCosts), "Enter premises staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.AgencySupplyTeachingStaffCosts), "Enter agency supply teaching staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.EducationSupportStaffCosts), "Enter education support staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.EducationalConsultancyCosts), "Enter educational consultancy in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.SupplyTeachingStaffCosts), "Enter supply teaching staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.TeachingStaffCosts), "Enter teaching staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.EnergyCosts), "Enter energy in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.WaterSewerageCosts), "Enter water and sewerage in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.DirectRevenueFinancingCosts), "Enter direct revenue financing in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.GroundsMaintenanceCosts), "Enter grounds maintenance in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.IndirectEmployeeExpenses), "Enter indirect employee expenses in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.InterestChargesLoanBank), "Enter interest charges for loan and bank in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.OtherInsurancePremiumsCosts), "Enter other insurance premiums in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.PrivateFinanceInitiativeCharges), "Enter private finance initiative (PFI) charges in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.RentRatesCosts), "Enter rent and rates in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.SpecialFacilitiesCosts), "Enter special facilities in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.StaffDevelopmentTrainingCosts), "Enter staff development and training in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.StaffRelatedInsuranceCosts), "Enter staff-related insurance in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.SupplyTeacherInsurableCosts), "Enter supply teacher insurance in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.TotalIncome), "Enter total income in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.TotalExpenditure), "Enter total spending in the correct format")
        );
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCustomDataFinancialData(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCustomDataFinancialData(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanPrePopulatePreviouslySubmittedValues()
    {
        var (page, school, customData) = await SetupNavigateInitPageWithUserData();
        AssertPageLayout(page, school);

        DocumentAssert.Input(page, "AdministrativeSuppliesCosts", customData.AdministrativeSuppliesNonEducationalCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "CateringStaffCosts", customData.CateringStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "CateringSuppliesCosts", customData.CateringSuppliesCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "ExaminationFeesCosts", customData.ExaminationFeesCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "LearningResourcesNonIctCosts", customData.LearningResourcesNonIctCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "LearningResourcesIctCosts", customData.LearningResourcesIctCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "AdministrativeClericalStaffCosts", customData.AdministrativeClericalStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "AuditorsCosts", customData.AuditorsCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "OtherStaffCosts", customData.OtherStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "ProfessionalServicesNonCurriculumCosts", customData.ProfessionalServicesNonCurriculumCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "CleaningCaretakingCosts", customData.CleaningCaretakingCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "MaintenancePremisesCosts", customData.MaintenancePremisesCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "OtherOccupationCosts", customData.OtherOccupationCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "PremisesStaffCosts", customData.PremisesStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "AgencySupplyTeachingStaffCosts", customData.AgencySupplyTeachingStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "EducationSupportStaffCosts", customData.EducationSupportStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "EducationalConsultancyCosts", customData.EducationalConsultancyCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "SupplyTeachingStaffCosts", customData.SupplyTeachingStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "TeachingStaffCosts", customData.TeachingStaffCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "EnergyCosts", customData.EnergyCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "WaterSewerageCosts", customData.WaterSewerageCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "DirectRevenueFinancingCosts", customData.DirectRevenueFinancingCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "GroundsMaintenanceCosts", customData.GroundsMaintenanceCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "IndirectEmployeeExpenses", customData.IndirectEmployeeExpenses.ToSimpleDisplay());
        DocumentAssert.Input(page, "InterestChargesLoanBank", customData.InterestChargesLoanBank.ToSimpleDisplay());
        DocumentAssert.Input(page, "OtherInsurancePremiumsCosts", customData.OtherInsurancePremiumsCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "PrivateFinanceInitiativeCharges", customData.PrivateFinanceInitiativeCharges.ToSimpleDisplay());
        DocumentAssert.Input(page, "RentRatesCosts", customData.RentRatesCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "SpecialFacilitiesCosts", customData.SpecialFacilitiesCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "StaffDevelopmentTrainingCosts", customData.StaffDevelopmentTrainingCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "StaffRelatedInsuranceCosts", customData.StaffRelatedInsuranceCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "SupplyTeacherInsurableCosts", customData.SupplyTeacherInsurableCosts.ToSimpleDisplay());
        DocumentAssert.Input(page, "TotalIncome", customData.TotalIncome.ToSimpleDisplay());
        DocumentAssert.Input(page, "TotalExpenditure", customData.TotalExpenditure.ToSimpleDisplay());

        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);
        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.Input(page, "NumberOfPupilsFte", customData.TotalPupils.ToSimpleDisplay());
        DocumentAssert.Input(page, "FreeSchoolMealPercent", customData.PercentFreeSchoolMeals.ToSimpleDisplay());
        DocumentAssert.Input(page, "SpecialEducationalNeedsPercent", customData.PercentSpecialEducationNeeds.ToSimpleDisplay());
        DocumentAssert.Input(page, "FloorArea", customData.TotalInternalFloorArea.ToSimpleDisplay());

        action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);
        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.Input(page, "WorkforceFte", customData.WorkforceFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "TeachersFte", customData.TeachersFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "QualifiedTeacherPercent", customData.PercentTeacherWithQualifiedStatus.ToSimpleDisplay());
        DocumentAssert.Input(page, "SeniorLeadershipFte", customData.SeniorLeadershipFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "TeachingAssistantsFte", customData.TeachingAssistantFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "NonClassroomSupportStaffFte", customData.NonClassroomSupportStaffFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "AuxiliaryStaffFte", customData.AuxiliaryStaffFTE.ToSimpleDisplay());
        DocumentAssert.Input(page, "WorkforceHeadcount", customData.WorkforceHeadcount.ToSimpleDisplay());
    }

    private (BenchmarkingWebAppClient client, School school) SetupClient()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var client = Client.SetupEstablishment(school)
            .SetupIncome(school, _income)
            .SetupCensus(school, _census)
            .SetupBalance(school, _balance)
            .SetupExpenditure(school, _expenditure)
            .SetupSchoolInsight(school)
            .SetupHttpContextAccessor();

        return (client, school);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var (client, school) = SetupClient();
        var doc = await client.Navigate(Paths.SchoolCustomDataFinancialData(school.URN));
        return (doc, school);
    }

    private async Task<(IHtmlDocument page, School school, PutCustomDataRequest customData)> SetupNavigateInitPageWithUserData()
    {
        var (client, school) = SetupClient();

        var userData = Fixture.Build<UserData>()
            .With(x => x.Type, "custom-data")
            .Create();
        client.SetupUserData([userData]);

        var customData = Fixture.Build<PutCustomDataRequest>().Create();
        var customDataSchool = new CustomDataSchool
        {
            Data = customData.ToJson()
        };
        client.SetUpCustomData(customDataSchool);

        var doc = await client.Navigate(Paths.SchoolCustomDataFinancialData(school.URN));
        return (doc, school, customData);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomData(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change financial data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(35, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(FinancialDataCustomDataViewModel.TotalIncome) => _income.TotalIncome.ToString(),
                nameof(FinancialDataCustomDataViewModel.TotalExpenditure) => _expenditure.TotalExpenditure.ToString(),
                _ => null // TODO : Explicitly set fields
            };

            if (expected != null && decimal.TryParse(expected, out var parsed))
            {
                Assert.Equal(parsed.ToCurrency(0), actual);
            }
        }
    }
}