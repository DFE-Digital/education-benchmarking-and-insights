using Web.App.Extensions;
namespace Web.App.Domain;

public record RagRating
{
    public RagRating(string? costGroup)
    {
        CostGroup = costGroup;
    }

    public string? Urn { get; set; }
    public int CostCategoryId { get; set; }
    public string? CostCategory { get; set; }
    public string CostCategoryAnchorId => string.IsNullOrWhiteSpace(CostCategory) ? string.Empty : CostCategory.ToSlug();
    public string? CostGroup { get; private set; }
    public decimal Value { get; set; }
    public decimal Median { get; set; }
    public int Decile { get; set; }
    public string? Status { get; set; }
    public int StatusOrder { get; set; }
    public (TagColour Colour, string DisplayText, string Class)? PriorityTag => Status != null ? Lookups.StatusPriorityMap[Status] : null;
    public string ResourcePartial => Lookups.CategoryResourcePartialMap[CostCategoryId];
    public string Unit => Lookups.CategoryUnitMap[CostCategoryId];

    public (decimal Difference, string Description) FromMedian
    {
        get
        {
            var diff = Value - Median;
            var text = diff < 0 ? "under" : "over";
            return (Math.Abs(diff), text);

        }
    }
}

public static class Lookups
{
    public static Dictionary<string, (TagColour Colour, string DisplayText, string Class)> StatusPriorityMap => new()
    {
        {
            "Red", (TagColour.Red, "High priority", "high")
        },
        {
            "Amber", (TagColour.Yellow, "Medium priority", "medium")
        },
        {
            "Green", (TagColour.Green, "Low priority", "low")
        }
    };

    public static Dictionary<int, string> CategoryResourcePartialMap => new()
    {
        {
            1, "CommercialResource/_TeachingStaff"
        },
        {
            2, "CommercialResource/_NonEducationalSupportStaff"
        },
        {
            3, "CommercialResource/_EducationalSupplies"
        },
        {
            4, "CommercialResource/_EducationalIct"
        },
        {
            5, "CommercialResource/_PremisesStaffServices"
        },
        {
            6, "CommercialResource/_Utilities"
        },
        {
            7, "CommercialResource/_AdministrativeSupplies"
        },
        {
            8, "CommercialResource/_CateringStaffServices"
        },
        {
            9, "CommercialResource/_OtherCosts"
        }
    };

    public static Dictionary<int, string> CategoryUnitMap => new()
    {
        {
            1, "per pupil"
        },
        {
            2, "per pupil"
        },
        {
            3, "per pupil"
        },
        {
            4, "per pupil"
        },
        {
            5, "per square metre"
        },
        {
            6, "per square metre"
        },
        {
            7, "per pupil"
        },
        {
            8, "per pupil"
        },
        {
            9, "per pupil"
        }
    };
}