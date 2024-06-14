using FluentValidation;
using Web.App.Domain.Benchmark.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages;

public class ManagementRolesStageValidator : AbstractValidator<ManagementRolesStage>
{
    public ManagementRolesStageValidator()
    {
        RuleFor(p => p)
            .Must(x => x.ManagementRoleHeadteacher || x.ManagementRoleDeputyHeadteacher ||
                       x.ManagementRoleNumeracyLead || x.ManagementRoleLiteracyLead ||
                       x.ManagementRoleHeadSmallCurriculum || x.ManagementRoleHeadKs1 ||
                       x.ManagementRoleHeadKs2 || x.ManagementRoleSenco ||
                       x.ManagementRoleAssistantHeadteacher || x.ManagementRoleHeadLargeCurriculum ||
                       x.ManagementRolePastoralLeader || x.ManagementRoleOtherMembers)
            .WithMessage("Select at least one management role")
            .WithName("ManagementRoles");
    }
}