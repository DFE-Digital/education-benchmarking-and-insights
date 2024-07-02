namespace Platform.Api.Insight.Census;

public static class CensusResponseFactory
{
    public static CensusResponse Create(CensusModel model, string? category, string dimension)
    {

        var response = CreateResponse<CensusResponse>(model, dimension, category);

        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;

        return response;
    }

    public static CensusHistoryResponse Create(CensusHistoryModel model, string dimension)
    {
        var response = CreateResponse<CensusHistoryResponse>(model, dimension);

        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(CensusBaseModel model, string dimension, string? category = null) where T : CensusBaseResponse, new()
    {
        var response = new T
        {
            URN = model.URN,
            TotalPupils = model.TotalPupils
        };

        if (category is null or CensusCategories.WorkforceFte)
        {
            response.Workforce = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.WorkforceFTE, model.WorkforceHeadcount)
                : CalculateValue(model.WorkforceFTE, model, dimension);
        }

        if (category is null or CensusCategories.WorkforceHeadcount)
        {
            response.WorkforceHeadcount = CalculateValue(model.WorkforceHeadcount, model, dimension);
        }

        if (category is null or CensusCategories.TeachersQualified)
        {
            response.PercentTeacherWithQualifiedStatus = model.PercentTeacherWithQualifiedStatus;
        }

        if (category is null or CensusCategories.TeachersFte)
        {
            response.Teachers = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.TeachersFTE, model.TeachersHeadcount)
                : CalculateValue(model.TeachersFTE, model, dimension);
        }

        if (category is null or CensusCategories.SeniorLeadershipFte)
        {
            response.SeniorLeadership = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.SeniorLeadershipFTE, model.SeniorLeadershipHeadcount)
                : CalculateValue(model.SeniorLeadershipFTE, model, dimension);
        }

        if (category is null or CensusCategories.TeachingAssistantsFte)
        {
            response.TeachingAssistant = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.TeachingAssistantFTE, model.TeachingAssistantHeadcount)
                : CalculateValue(model.TeachingAssistantFTE, model, dimension);
        }

        if (category is null or CensusCategories.NonClassroomSupportStaffFte)
        {
            response.NonClassroomSupportStaff = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.NonClassroomSupportStaffFTE, model.NonClassroomSupportStaffHeadcount)
                : CalculateValue(model.NonClassroomSupportStaffFTE, model, dimension);
        }

        if (category is null or CensusCategories.AuxiliaryStaffFte)
        {
            response.AuxiliaryStaff = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.AuxiliaryStaffFTE, model.AuxiliaryStaffHeadcount)
                : CalculateValue(model.AuxiliaryStaffFTE, model, dimension);
        }

        return response;
    }

    private static decimal? CalculateValue(decimal? value, CensusBaseModel model, string dimension)
    {
        if (value == null)
        {
            return value;
        }

        return dimension switch
        {
            CensusDimensions.Total => value,
            CensusDimensions.HeadcountPerFte => CalculateValue(model.WorkforceHeadcount, value),
            CensusDimensions.PercentWorkforce => CalculateValue(value, model.WorkforceFTE) * 100,
            CensusDimensions.PupilsPerStaffRole => CalculateValue(model.TotalPupils, value),
            _ => null
        };
    }

    private static decimal? CalculateValue(decimal? numerator, decimal? denominator)
    {
        if (denominator == null)
        {
            return denominator;
        }

        return denominator != 0 ? numerator / denominator : 0;
    }
}


public abstract record CensusBaseResponse
{
    public string? URN { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Workforce { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? Teachers { get; set; }
    public decimal? SeniorLeadership { get; set; }
    public decimal? TeachingAssistant { get; set; }
    public decimal? NonClassroomSupportStaff { get; set; }
    public decimal? AuxiliaryStaff { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
}

public record CensusResponse : CensusBaseResponse
{
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record CensusHistoryResponse : CensusBaseResponse
{
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}