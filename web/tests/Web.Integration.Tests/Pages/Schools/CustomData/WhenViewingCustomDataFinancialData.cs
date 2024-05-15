using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.ViewModels;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataFinancialData : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census _census;
    private readonly Expenditure _customExpenditure;
    private readonly Income _customIncome;
    private readonly Expenditure _expenditure;
    private readonly Finances _finances;
    private readonly FloorAreaMetric _floorAreaMetric;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly Income _income;

    public WhenViewingCustomDataFinancialData(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _finances = Fixture.Build<Finances>()
            .Create();

        _income = Fixture.Build<Income>()
            .Create();

        _customIncome = Fixture.Build<Income>()
            .Create();

        _expenditure = Fixture.Build<Expenditure>()
            .Create();

        _customExpenditure = Fixture.Build<Expenditure>()
            .Create();

        _floorAreaMetric = Fixture.Build<FloorAreaMetric>()
            .Create();

        _census = Fixture.Build<Census>()
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
                nameof(FinancialDataCustomDataViewModel.CateringIncome), _customIncome.IncomeCatering
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
                nameof(FinancialDataCustomDataViewModel.TotalIncome), _finances.TotalIncome
            },
            {
                nameof(FinancialDataCustomDataViewModel.TotalExpenditure), _finances.TotalExpenditure
            },
            {
                nameof(FinancialDataCustomDataViewModel.RevenueReserve), _finances.RevenueReserve
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
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
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        // ... and then back again
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.Urn).ToAbsolute());
        var customValues = page.QuerySelectorAll("input").Not("[type='hidden']").ToList();
        Assert.Equal(33, customValues.Count);

        foreach (var customValue in customValues)
        {
            var actual = (customValue as IHtmlInputElement)?.Value;
            var field = customValue.Id ?? string.Empty;
            var expected = field switch
            {
                nameof(FinancialDataCustomDataViewModel.CateringIncome) => _customIncome.IncomeCatering,
                nameof(FinancialDataCustomDataViewModel.TotalExpenditure) => _customExpenditure.TotalExpenditure,
                _ => _customExpenditure.GetType().GetProperty(field)?.GetValue(_customExpenditure)
            };

            if (expected != null && decimal.TryParse(expected.ToString(), out var parsed))
            {
                expected = parsed.ToString("#.0");
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.Urn).ToAbsolute());
        DocumentAssert.SummaryErrors(
            page,
            (nameof(FinancialDataCustomDataViewModel.AdministrativeSuppliesCosts), "Enter administrative supplies (non-educational) in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CateringStaffCosts), "Enter catering staff in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CateringSuppliesCosts), "Enter catering supplies in the correct format"),
            (nameof(FinancialDataCustomDataViewModel.CateringIncome), "Enter income from catering in the correct format"),
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
            (nameof(FinancialDataCustomDataViewModel.SupplyTeacherInsurableCosts), "Enter supply teacher insurance in the correct format")
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.Urn, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, _finances, _income, _expenditure, _floorAreaMetric)
            .SetupCensus(school, _census)
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataFinancialData(school.Urn));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomData(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change financial data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(36, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(FinancialDataCustomDataViewModel.CateringIncome) => _income.IncomeCatering,
                nameof(FinancialDataCustomDataViewModel.TotalExpenditure) => _finances.TotalExpenditure,
                _ => _expenditure.GetType().GetProperty(field)?.GetValue(_expenditure)
                     ?? _finances.GetType().GetProperty(field)?.GetValue(_finances)
            };

            if (expected != null && decimal.TryParse(expected.ToString(), out var parsed))
            {
                expected = parsed.ToCurrency();
            }

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}