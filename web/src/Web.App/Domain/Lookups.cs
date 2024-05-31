namespace Web.App.Domain;

public static class Lookups
{
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

    public static Dictionary<string, string> CategoryResourcePartialMap => new()
    {
        { Category.TeachingStaff, "CommercialResource/_TeachingStaff" },
        { Category.NonEducationalSupportStaff, "CommercialResource/_NonEducationalSupportStaff" },
        { Category.EducationalSupplies, "CommercialResource/_EducationalSupplies" },
        { Category.EducationalIct, "CommercialResource/_EducationalIct" },
        { Category.PremisesStaffServices, "CommercialResource/_PremisesStaffServices" },
        { Category.Utilities, "CommercialResource/_Utilities" },
        { Category.AdministrativeSupplies, "CommercialResource/_AdministrativeSupplies" },
        { Category.CateringStaffServices, "CommercialResource/_CateringStaffServices" },
        { Category.Other, "CommercialResource/_OtherCosts" }
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