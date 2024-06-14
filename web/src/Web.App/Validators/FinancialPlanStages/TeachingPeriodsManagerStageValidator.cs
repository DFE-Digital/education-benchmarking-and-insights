using FluentValidation;
using Web.App.Domain.Benchmark.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TeachingPeriodsManagerStageValidator : AbstractValidator<TeachingPeriodsManagerStage>
{
    public TeachingPeriodsManagerStageValidator()
    {
        When(p => p.ManagementRoleHeadteacher, () =>
        {
            RuleForEach(p => p.TeachingPeriodsHeadteacher)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods of headteachers must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods of headteachers must be 1 or more")
                .WithName("TeachingPeriodsHeadteacher");
        });

        When(p => p.ManagementRoleDeputyHeadteacher, () =>
        {
            RuleForEach(p => p.TeachingPeriodsDeputyHeadteacher)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for deputy headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for deputy headteachers must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for deputy headteachers must be 1 or more")
                .WithName("TeachingPeriodsDeputyHeadteacher");
        });

        When(p => p.ManagementRoleNumeracyLead, () =>
        {
            RuleForEach(p => p.TeachingPeriodsNumeracyLead)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for numeracy leads")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for numeracy leads must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for numeracy leads must be 1 or more")
                .WithName("TeachingPeriodsNumeracyLead");
        });

        When(p => p.ManagementRoleLiteracyLead, () =>
        {
            RuleForEach(p => p.TeachingPeriodsLiteracyLead)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for literacy leads")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for literacy leads must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for literacy leads must be 1 or more")
                .WithName("TeachingPeriodsLiteracyLead");
        });

        When(p => p.ManagementRoleHeadSmallCurriculum, () =>
        {
            RuleForEach(p => p.TeachingPeriodsHeadSmallCurriculum)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for heads of small curriculum")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for heads of small curriculum must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for heads of small curriculum must be 1 or more")
                .WithName("TeachingPeriodsHeadSmallCurriculum");
        });

        When(p => p.ManagementRoleHeadKs1, () =>
        {
            RuleForEach(p => p.TeachingPeriodsHeadKs1)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for heads of KS1")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for heads of KS1 must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for heads of KS1 must be 1 or more")
                .WithName("TeachingPeriodsHeadKs1");
        });

        When(p => p.ManagementRoleHeadKs2, () =>
        {
            RuleForEach(p => p.TeachingPeriodsHeadKs2)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for heads of KS2")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for heads of KS2 must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for heads of KS2 must be 1 or more")
                .WithName("TeachingPeriodsHeadKs2");
        });

        When(p => p.ManagementRoleSenco, () =>
        {
            RuleForEach(p => p.TeachingPeriodsSenco)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for special education needs coordinators")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for special education needs coordinators must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for special education needs coordinators must be 1 or more")
                .WithName("TeachingPeriodsSenco");
        });

        When(p => p.ManagementRoleAssistantHeadteacher, () =>
        {
            RuleForEach(p => p.TeachingPeriodsAssistantHeadteacher)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for assistant Headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for assistant Headteachers must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for assistant Headteachers must be 1 or more")
                .WithName("TeachingPeriodsAssistantHeadteacher");
        });

        When(p => p.ManagementRoleHeadLargeCurriculum, () =>
        {
            RuleForEach(p => p.TeachingPeriodsHeadLargeCurriculum)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for heads of large curriculum")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for heads of large curriculum must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for heads of large curriculum must be 1 or more")
                .WithName("TeachingPeriodsHeadLargeCurriculum");
        });

        When(p => p.ManagementRolePastoralLeader, () =>
        {
            RuleForEach(p => p.TeachingPeriodsPastoralLeader)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for pastoral leaders")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for pastoral leaders must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for pastoral leaders must be 1 or more")
                .WithName("TeachingPeriodsPastoralLeader");
        });

        When(p => p.ManagementRoleOtherMembers, () =>
        {
            RuleForEach(p => p.TeachingPeriodsOtherMembers)
                .OverrideIndexer((_, _, _, _) => string.Empty)
                .NotEmpty()
                .WithMessage("Enter the number of teaching periods for other members of management or leadership staff")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Number of teaching periods for other members of management or leadership staff must be a whole number")
                .Must(x => x.ToInt() > 0)
                .WithMessage("Number of teaching periods for other members of management or leadership staff must be 1 or more")
                .WithName("TeachingPeriodsOtherMembers");
        });
    }
}