using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure.ExpenditureResponseFactoryCreate;

public class WhenExpenditureResponseFactoryCreatesResponseFromSchoolExpenditure
{
    private readonly SchoolExpenditureModel _model;

    public WhenExpenditureResponseFactoryCreatesResponseFromSchoolExpenditure()
    {
        _model = TestDataReader.ReadTestDataFromFile<SchoolExpenditureModel>("ExpenditureTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalExpenditureTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithTotalExpenditure(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertTotalExpenditure(expected, response);
    }

    [Theory]
    [ClassData(typeof(TeachingTeachingSupportStaffTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithTeachingTeachingSupportStaff(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertTeachingTeachingSupportStaff(expected, response);
    }

    [Theory]
    [ClassData(typeof(NonEducationalSupportStaffTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithNonEducationalSupportStaff(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertNonEducationalSupportStaff(expected, response);
    }

    [Theory]
    [ClassData(typeof(EducationalSuppliesTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithEducationalSupplies(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        // act
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertEducationalSupplies(expected, response);
    }

    [Theory]
    [ClassData(typeof(EducationalIctTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithEducationalIct(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertEducationalIct(expected, response);
    }

    [Theory]
    [ClassData(typeof(PremisesStaffServicesTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithPremisesStaffServices(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertPremisesStaffServices(expected, response);
    }

    [Theory]
    [ClassData(typeof(UtilitiesTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithUtilities(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertUtilities(expected, response);
    }

    [Theory]
    [ClassData(typeof(AdministrationSuppliesTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithAdministrationSupplies(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertAdministrationSupplies(expected, response);
    }

    [Theory]
    [ClassData(typeof(CateringStaffServicesTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithCateringStaffServices(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertCateringStaffServices(expected, response);
    }

    [Theory]
    [ClassData(typeof(OtherCostsTestData<SchoolExpenditureResponse>))]
    public void ShouldBuildResponseModelWithOtherCosts(string? category, string dimension, bool excludeCentralServices, ExpenditureBaseResponse expected)
    {
        var response = ExpenditureResponseFactory.Create(_model, new ExpenditureParameters
        {
            ExcludeCentralServices = excludeCentralServices,
            Category = category,
            Dimension = dimension
        });

        Assertions.AssertOtherCosts(expected, response);
    }
}