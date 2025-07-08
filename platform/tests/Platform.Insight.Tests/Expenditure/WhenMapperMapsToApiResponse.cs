using AutoFixture;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Shared;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class WhenMapperMapsToApiResponse
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldThrowArgumentNullExceptionForNullSchoolModel()
    {
        ExpenditureSchoolModel model = null!;

        Assert.Throws<ArgumentNullException>(() => model.MapToApiResponse());
    }

    [Fact]
    public void ShouldMapBasicPropertiesForValidSchoolModel()
    {
        var model = _fixture.Create<ExpenditureSchoolModel>();

        var result = model.MapToApiResponse();

        Assert.Equal(model.URN, result.URN);
        Assert.Equal(model.SchoolName, result.SchoolName);
        Assert.Equal(model.SchoolType, result.SchoolType);
        Assert.Equal(model.LAName, result.LAName);
        Assert.Equal(model.TotalPupils, result.TotalPupils);
        Assert.Equal(model.PeriodCoveredByReturn, result.PeriodCoveredByReturn);
        Assert.Equal(model.TotalInternalFloorArea, result.TotalInternalFloorArea);
        Assert.Equal(model.TotalExpenditure, result.TotalExpenditure);
    }

    [Fact]
    public void ShouldMapTeachingStaffPropertiesForTeachingStaffCategory()
    {
        var model = _fixture.Create<ExpenditureSchoolModel>();

        var result = model.MapToApiResponse(Categories.Cost.TeachingTeachingSupportStaff);

        Assert.Equal(model.TotalTeachingSupportStaffCosts, result.TotalTeachingSupportStaffCosts);
        Assert.Equal(model.TeachingStaffCosts, result.TeachingStaffCosts);
        Assert.Equal(model.SupplyTeachingStaffCosts, result.SupplyTeachingStaffCosts);
        Assert.Equal(model.EducationalConsultancyCosts, result.EducationalConsultancyCosts);
        Assert.Equal(model.EducationSupportStaffCosts, result.EducationSupportStaffCosts);
        Assert.Equal(model.AgencySupplyTeachingStaffCosts, result.AgencySupplyTeachingStaffCosts);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionForNullTrustModel()
    {
        ExpenditureTrustModel model = null!;

        Assert.Throws<ArgumentNullException>(() => model.MapToApiResponse());
    }

    [Fact]
    public void ShouldMapBasicPropertiesForValidTrustModel()
    {
        var model = _fixture.Create<ExpenditureTrustModel>();

        var result = model.MapToApiResponse();

        Assert.Equal(model.TrustName, result.TrustName);
        Assert.Equal(model.CompanyNumber, result.CompanyNumber);
        Assert.Equal(model.TotalExpenditure, result.TotalExpenditure);
        Assert.Equal(model.TotalExpenditureCS, result.CentralTotalExpenditure);
        Assert.Equal(model.TotalExpenditureSchool, result.SchoolTotalExpenditure);
        Assert.Equal(model.TotalPupils, result.TotalPupils);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData("abc", null)]
    [InlineData("123", null)]
    [InlineData("EMLBANDS050", 50)]
    [InlineData("EMLBANDS250", 250)]
    public void ShouldMapHighestSalaryEmolumentBandValueForValidTrustModelWithEmlBand(string? emlBand, int? expected)
    {
        var model = new ExpenditureTrustModel
        {
            EmlBand = emlBand
        };

        var result = model.MapToApiResponse();

        Assert.Equal(expected, result.HighestSalaryEmolumentBandValue);
    }

    [Fact]
    public void ShouldExcludeCentralServiceDataForExcludeCentralService()
    {
        var model = _fixture.Create<ExpenditureTrustModel>();

        var result = model.MapToApiResponse(excludeCentralService: true);

        Assert.Equal(model.TrustName, result.TrustName);
        Assert.Equal(model.CompanyNumber, result.CompanyNumber);
        Assert.Equal(model.TotalExpenditureSchool, result.TotalExpenditure);
        Assert.Null(result.CentralTotalExpenditure);
        Assert.Null(result.SchoolTotalExpenditure);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionForNullTrustModels()
    {
        ExpenditureTrustModel model = null!;
        var models = new List<ExpenditureTrustModel>
        {
            model
        };

        var results = models.MapToApiResponse();
        Assert.Throws<ArgumentNullException>(() => results.ToList());
    }

    [Fact]
    public void ShouldMapCorrectlyForTrustModels()
    {
        var models = _fixture.Build<ExpenditureTrustModel>().CreateMany().ToList();

        var result = models.MapToApiResponse().ToList();

        Assert.Equal(models.Count, result.Count);
        Assert.Equal(models.First().TotalExpenditure, result.First().TotalExpenditure);
    }

    [Fact]
    public void ShouldMapAllModelsForMultipleSchoolModels()
    {
        var models = _fixture.Build<ExpenditureSchoolModel>().CreateMany().ToList();

        var results = models.MapToApiResponse().ToList();

        Assert.Equal(models.Count, results.Count);
        for (var i = 0; i < models.Count; i++)
        {
            Assert.Equal(models.ElementAt(i).URN, results.ElementAt(i).URN);
        }
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionForNullHistoryModels()
    {
        var years = new YearsModel
        {
            StartYear = 2020,
            EndYear = 2024
        };

        ExpenditureHistoryModel model = null!;
        var models = new List<ExpenditureHistoryModel>
        {
            model
        };

        var results = years.MapToApiResponse(models);
        Assert.Throws<ArgumentNullException>(() => results.Rows.ToList());
    }

    [Fact]
    public void ShouldMapCorrectlyForHistoryModels()
    {
        var years = new YearsModel
        {
            StartYear = 2020,
            EndYear = 2024
        };
        var models = new List<ExpenditureHistoryModel>
        {
            _fixture.Build<ExpenditureHistoryModel>()
                .With(x => x.RunId, 2020)
                .Create(),
            _fixture.Build<ExpenditureHistoryModel>()
                .With(x => x.RunId, 2021)
                .Create()
        };

        var result = years.MapToApiResponse(models);

        Assert.Equal(years.StartYear, result.StartYear);
        Assert.Equal(years.EndYear, result.EndYear);
        Assert.Equal(models.Count, result.Rows.Count());
        Assert.Equal(models.First().TotalExpenditure, result.Rows.First().TotalExpenditure);
    }
}