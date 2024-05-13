namespace Web.App.ViewModels;

public interface IWorkforceDataCustomDataViewModel
{
    decimal? WorkforceFte { get; }
    decimal? TeachersFte { get; }
    decimal? SeniorLeadershipFte { get; }
}

public record WorkforceDataCustomDataViewModel : IWorkforceDataCustomDataViewModel
{
    public decimal? WorkforceFte { get; init; }
    public decimal? TeachersFte { get; init; }
    public decimal? SeniorLeadershipFte { get; init; }
}