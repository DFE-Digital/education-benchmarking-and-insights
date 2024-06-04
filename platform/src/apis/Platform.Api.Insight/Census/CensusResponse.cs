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
            response.WorkforceFTE = CalculateValue(model.WorkforceFTE, model, dimension);
        }

        if (category is null or CensusCategories.TeachersQualified)
        {
            response.PercentTeacherWithQualifiedStatus = model.PercentTeacherWithQualifiedStatus;
        }

        if (category is null or CensusCategories.TeachersFte)
        {
            response.TeachersFTE = CalculateValue(model.TeachersFTE, model, dimension);
        }

        if (category is null or CensusCategories.SeniorLeadershipFte)
        {
            response.SeniorLeadershipFTE = CalculateValue(model.SeniorLeadershipFTE, model, dimension);
        }

        if (category is null or CensusCategories.TeachingAssistantsFte)
        {
            response.TeachingAssistantFTE = CalculateValue(model.TeachingAssistantFTE, model, dimension);
        }

        if (category is null or CensusCategories.NonClassroomSupportStaffFte)
        {
            response.NonClassroomSupportStaffFTE = CalculateValue(model.NonClassroomSupportStaffFTE, model, dimension);
        }

        if (category is null or CensusCategories.AuxiliaryStaffFte)
        {
            response.AuxiliaryStaffFTE = CalculateValue(model.AuxiliaryStaffFTE, model, dimension);
        }

        if (category is null or CensusCategories.WorkforceHeadcount)
        {
            response.WorkforceHeadcount = CalculateValue(model.WorkforceHeadcount, model, dimension);
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
            CensusDimensions.HeadcountPerFte => value != 0 ? model.WorkforceHeadcount / value : 0,
            CensusDimensions.PercentWorkforce => model.WorkforceFTE != 0 ? value / model.WorkforceFTE * 100 : 0,
            CensusDimensions.PupilsPerStaffRole => value != 0 ? model.TotalPupils / value : 0,
            _ => null
        };
    }
}


public abstract record CensusBaseResponse
{
    public string? URN { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? WorkforceFTE { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? TeachersFTE { get; set; }
    public decimal? SeniorLeadershipFTE { get; set; }
    public decimal? TeachingAssistantFTE { get; set; }
    public decimal? NonClassroomSupportStaffFTE { get; set; }
    public decimal? AuxiliaryStaffFTE { get; set; }
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