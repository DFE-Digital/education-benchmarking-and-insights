using Platform.Api.Insight.Expenditure;
using Xunit;
namespace Platform.Tests.Insight.Expenditure.ExpenditureResponseFactoryCreate;

public class WhenExpenditureResponseFactoryCreatesResponseFromTrustExpenditure
{
    private readonly TrustExpenditureModel _model;

    public WhenExpenditureResponseFactoryCreatesResponseFromTrustExpenditure()
    {
        _model = TestDataReader.ReadTestDataFromFile<TrustExpenditureModel>("ExpenditureTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(TotalExpenditureTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(TeachingTeachingSupportStaffTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(NonEducationalSupportStaffTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(EducationalSuppliesTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(EducationalIctTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(PremisesStaffServicesTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(UtilitiesTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(AdministrationSuppliesTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(CateringStaffServicesTestData<TrustExpenditureResponse>))]
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
    [ClassData(typeof(OtherCostsTestData<TrustExpenditureResponse>))]
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