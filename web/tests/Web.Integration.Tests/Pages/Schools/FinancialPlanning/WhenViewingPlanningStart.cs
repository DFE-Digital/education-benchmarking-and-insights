﻿using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningStart(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);

        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToHelp(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var anchor = page.QuerySelector("main .govuk-grid-row .govuk-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningHelp(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToContinue(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
        var anchor = page.QuerySelector("main .govuk-button");
        page = await Client.Follow(anchor);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningSelectYear(school.URN).ToAbsolute());
    }


    /*[Fact]
    // [InlineData(EstablishmentTypes.Academies)]
    // [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());#1#
    }*/

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningStart(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningStart(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningStart(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolFinancialPlanningStart(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Curriculum and financial planning (CFP) - Financial Benchmarking and Insights Tool - GOV.UK", "Curriculum and financial planning (CFP)");

        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolFinancialPlanningSelectYear(school.URN));

        var helpLink = page.QuerySelector("main .govuk-grid-row .govuk-link");
        DocumentAssert.Link(helpLink, "can be found here", Paths.SchoolFinancialPlanningHelp(school.URN).ToAbsolute());
    }
}