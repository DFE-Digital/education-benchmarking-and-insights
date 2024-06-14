using FluentValidation;
using Web.App.Domain.Benchmark.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class ManagersPerRoleStageValidator : AbstractValidator<ManagersPerRoleStage>
{
    public ManagersPerRoleStageValidator()
    {
        When(p => p.ManagementRoleHeadteacher, () =>
        {
            RuleFor(p => p.NumberHeadteacher)
                .NotEmpty()
                .WithMessage("Enter your total number of headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of headteachers must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of headteachers must be 1 or more");
        });

        When(p => p.ManagementRoleDeputyHeadteacher, () =>
        {
            RuleFor(p => p.NumberDeputyHeadteacher)
                .NotEmpty()
                .WithMessage("Enter your total number of deputy headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of deputy headteachers must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of deputy headteachers must be 1 or more");
        });

        When(p => p.ManagementRoleNumeracyLead, () =>
        {
            RuleFor(p => p.NumberNumeracyLead)
                .NotEmpty()
                .WithMessage("Enter your total number of numeracy leads")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of numeracy leads must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of numeracy leads must be 1 or more");
        });

        When(p => p.ManagementRoleLiteracyLead, () =>
        {
            RuleFor(p => p.NumberLiteracyLead)
                .NotEmpty()
                .WithMessage("Enter your total number of literacy leads")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of literacy leads must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of literacy leads must be 1 or more");
        });

        When(p => p.ManagementRoleHeadSmallCurriculum, () =>
        {
            RuleFor(p => p.NumberHeadSmallCurriculum)
                .NotEmpty()
                .WithMessage("Enter your total number of head of small curriculum areas")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of head of small curriculum areas must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of head of small curriculum areas must be 1 or more");
        });

        When(p => p.ManagementRoleHeadKs1, () =>
        {
            RuleFor(p => p.NumberHeadKs1)
                .NotEmpty()
                .WithMessage("Enter your total number of heads of KS1")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of heads of KS1 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of heads of KS1 must be 1 or more");
        });

        When(p => p.ManagementRoleHeadKs2, () =>
        {
            RuleFor(p => p.NumberHeadKs2)
                .NotEmpty()
                .WithMessage("Enter your total number of heads of KS2")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of heads of KS2 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of heads of KS2 must be 1 or more");
        });

        When(p => p.ManagementRoleSenco, () =>
        {
            RuleFor(p => p.NumberSenco)
                .NotEmpty()
                .WithMessage("Enter your total number of special education needs coordinators")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of special education needs coordinators must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of special education needs coordinators must be 1 or more");
        });

        When(p => p.ManagementRoleAssistantHeadteacher, () =>
        {
            RuleFor(p => p.NumberAssistantHeadteacher)
                .NotEmpty()
                .WithMessage("Enter your total number of assistant headteachers")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of assistant headteachers must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of assistant headteachers must be 1 or more");
        });

        When(p => p.ManagementRoleHeadLargeCurriculum, () =>
        {
            RuleFor(p => p.NumberHeadLargeCurriculum)
                .NotEmpty()
                .WithMessage("Enter your total number of head of large curriculum areas")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of head of large curriculum areas must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of head of large curriculum areas must be 1 or more");
        });

        When(p => p.ManagementRolePastoralLeader, () =>
        {
            RuleFor(p => p.NumberPastoralLeader)
                .NotEmpty()
                .WithMessage("Enter your total number of pastoral leaders")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of pastoral leaders must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of pastoral leaders must be 1 or more");
        });

        When(p => p.ManagementRoleOtherMembers, () =>
        {
            RuleFor(p => p.NumberOtherMembers)
                .NotEmpty()
                .WithMessage("Enter your total number of other members of management or leadership staff")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Total number of other members of management or leadership staff must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Total number of other members of management or leadership staff must be 1 or more");
        });
    }
}