using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenAnAdministrativeSuppliesCostCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new AdministrativeSupplies(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenACateringStaffServicesCostCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new CateringStaffServices(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenAnEducationalIctCategory
{
    [Theory]
    [InlineData("red", 0, 1, false)]
    [InlineData("amber", 1, 0, true)]
    [InlineData("amber", 1, 1, true)]
    [InlineData("green", null, null, false)]
    public void WhenRagRatingIs(string rag, int? value, int? median, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag,
            Value = value,
            Median = median
        };
        var category = new EducationalIct(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenAnEducationalSuppliesCategory
{
    [Theory]
    [InlineData("red", 0, 1, false)]
    [InlineData("amber", 1, 0, true)]
    [InlineData("amber", 1, 1, true)]
    [InlineData("green", null, null, false)]
    public void WhenRagRatingIs(string rag, int? value, int? median, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag,
            Value = value,
            Median = median
        };
        var category = new EducationalSupplies(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenNonEducationalSupportStaffCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new NonEducationalSupportStaff(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenTeachingStaffCategory
{
    [Theory]
    [InlineData("red", 0, 1, false)]
    [InlineData("amber", 1, 0, true)]
    [InlineData("amber", 1, 1, true)]
    [InlineData("green", null, null, false)]
    public void WhenRagRatingIs(string rag, int? value, int? median, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag,
            Value = value,
            Median = median
        };
        var category = new TeachingStaff(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenOtherCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new Other(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenPremisesStaffServicesCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new PremisesStaffServices(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}

public class GivenUtilitiesCategory
{
    [Theory]
    [InlineData("red", true)]
    [InlineData("amber", true)]
    [InlineData("green", false)]
    public void WhenRagRatingIs(string rag, bool expectedCanShowCommercialResources)
    {
        var ragRating = new RagRating
        {
            RAG = rag
        };
        var category = new Utilities(ragRating);

        Assert.Equal(expectedCanShowCommercialResources, category.CanShowCommercialResources);
    }
}