namespace Web.App.Domain;

public record RagRating
{
    public string? Urn { get; set; }
    public int CostCategoryId { get; set; }
    public string? CostCategory { get; set; }
    public decimal Value { get; set; }
    public decimal Median { get; set; }
    public int Decile { get; set; }
    public string? Status { get; set; }
    public int StatusOrder { get; set; }
}

public static class Lookups
{
    public static Dictionary<string, (TagColour, string)> StatusPriorityMap => new()
    {
        { "Red", (TagColour.Red, "High priority") },
        { "Amber", (TagColour.Yellow, "Medium priority") },
        { "Green", (TagColour.Grey, "Low priority") },
    };

    public static Dictionary<int, string> CategoryResourcePartialMap => new()
    {
        { 1, "CommercialResource/_TeachingStaff" },
        { 2, "CommercialResource/_NonEducationalSupportStaff" },
        { 3, "CommercialResource/_EducationalSupplies" },
        { 4, "CommercialResource/_EducationalIct" },
        { 5, "CommercialResource/_PremisesStaffServices" },
        { 6, "CommercialResource/_Utilities" },
        { 7, "CommercialResource/_AdministrativeSupplies" },
        { 8, "CommercialResource/_CateringStaffServices" },
        { 9, "CommercialResource/_OtherCosts" }
    };
}