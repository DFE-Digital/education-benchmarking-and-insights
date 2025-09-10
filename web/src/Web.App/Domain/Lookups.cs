namespace Web.App.Domain;

public static class Lookups
{
    public static readonly Dictionary<string, string[]> CategorySubCategoryMap = new()
    {
        { Category.TeachingStaff, SubCostCategories.TeachingStaff.SubCategories },
        { Category.NonEducationalSupportStaff, SubCostCategories.NonEducationalSupportStaff.SubCategories },
        { Category.EducationalSupplies, SubCostCategories.EducationalSupplies.SubCategories },
        { Category.EducationalIct, SubCostCategories.EducationalIct.SubCategories },
        { Category.PremisesStaffServices, SubCostCategories.PremisesStaffServices.SubCategories },
        { Category.Utilities, SubCostCategories.Utilities.SubCategories },
        { Category.AdministrativeSupplies, SubCostCategories.AdministrativeSupplies.SubCategories },
        { Category.CateringStaffServices, SubCostCategories.CateringStaffServices.SubCategories },
        { Category.Other, SubCostCategories.Other.SubCategories }
    };

    public static Dictionary<string, (TagColour Colour, string DisplayText, string Class)> StatusPriorityMap => new()
    {
        { "red", (TagColour.Red, "High priority", "high") },
        { "amber", (TagColour.Yellow, "Medium priority", "medium") },
        { "green", (TagColour.Green, "Low priority", "low") }
    };

    public static Dictionary<string, int> StatusOrderMap => new()
    {
        { "red", 1 },
        { "amber", 2 },
        { "green", 3 }
    };

    public static Dictionary<string, string> CategoryTypeMap => new()
    {
        { Category.TeachingStaff, "Pupil" },
        { Category.NonEducationalSupportStaff, "Pupil" },
        { Category.EducationalSupplies, "Pupil" },
        { Category.EducationalIct, "Pupil" },
        { Category.PremisesStaffServices, "Building" },
        { Category.Utilities, "Building" },
        { Category.AdministrativeSupplies, "Pupil" },
        { Category.CateringStaffServices, "Pupil" },
        { Category.Other, "Pupil" }
    };

    public static Dictionary<string, int> CategoryOrderMap => new()
    {
        { Category.TeachingStaff, 1 },
        { Category.NonEducationalSupportStaff, 2 },
        { Category.EducationalSupplies, 3 },
        { Category.EducationalIct, 4 },
        { Category.PremisesStaffServices, 5 },
        { Category.Utilities, 6 },
        { Category.AdministrativeSupplies, 7 },
        { Category.CateringStaffServices, 8 },
        { Category.Other, 9 }
    };

    public static Dictionary<string, string> CategoryUnitMap => new()
    {
        { Category.TeachingStaff, "per pupil" },
        { Category.NonEducationalSupportStaff, "per pupil" },
        { Category.EducationalSupplies, "per pupil" },
        { Category.EducationalIct, "per pupil" },
        { Category.PremisesStaffServices, "per square metre" },
        { Category.Utilities, "per square metre" },
        { Category.AdministrativeSupplies, "per pupil" },
        { Category.CateringStaffServices, "per pupil" },
        { Category.Other, "per pupil" }
    };
}