using Web.App.Extensions;

namespace Web.App.Domain.FinancialPlanStages;

public class PupilFiguresStage : Stage
{
    public string? PupilsYear7 { get; set; }
    public string? PupilsYear8 { get; set; }
    public string? PupilsYear9 { get; set; }
    public string? PupilsYear10 { get; set; }
    public string? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.PupilsYear7 = PupilsYear7;
        planInput.PupilsYear8 = PupilsYear8;
        planInput.PupilsYear9 = PupilsYear9;
        planInput.PupilsYear10 = PupilsYear10;
        planInput.PupilsYear11 = PupilsYear11;
        planInput.PupilsYear12 = PupilsYear12;
        planInput.PupilsYear13 = PupilsYear13;

        planInput.TeachersYear7 = PupilsYear7.ToInt() > 0 ? planInput.TeachersYear7 : null;
        planInput.TeachersYear8 = PupilsYear8.ToInt() > 0 ? planInput.TeachersYear8 : null;
        planInput.TeachersYear9 = PupilsYear9.ToInt() > 0 ? planInput.TeachersYear9 : null;
        planInput.TeachersYear10 = PupilsYear10.ToInt() > 0 ? planInput.TeachersYear10 : null;
        planInput.TeachersYear11 = PupilsYear11.ToInt() > 0 ? planInput.TeachersYear11 : null;
        planInput.TeachersYear12 = PupilsYear12 > 0 ? planInput.TeachersYear12 : null;
        planInput.TeachersYear13 = PupilsYear13 > 0 ? planInput.TeachersYear13 : null;
    }
}