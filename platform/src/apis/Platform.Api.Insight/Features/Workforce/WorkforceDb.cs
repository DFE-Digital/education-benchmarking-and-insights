using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Features.Workforce;

public interface IWorkforceDb
{
    Task<IEnumerable<WorkforceResponseModel>> GetHistory(string urn, WorkforceHistoryQueryParameters parameters);
    Task<IEnumerable<WorkforceResponseModel>> Get(WorkforceQueryParameters parameters);
}

public class WorkforceDb(IOptions<DbOptions> options, ICosmosClientFactory factory)
    : FinancesDb(options.Value, factory), IWorkforceDb
{
    public async Task<IEnumerable<WorkforceResponseModel>> GetHistory(string urn, WorkforceHistoryQueryParameters parameters)
    {
        var finances = await GetFinancesHistory<WorkforceDataObject>(urn);

        return finances
            .OfType<(int Term, WorkforceDataObject? DataObject)>()
            .Select(x => WorkforceResponseModel.Create(x.DataObject, x.Term, parameters.Dimension));
    }

    public async Task<IEnumerable<WorkforceResponseModel>> Get(WorkforceQueryParameters parameters)
    {
        var finances = await GetFinances<WorkforceDataObject>(parameters.Urns);
        return parameters.Category.ToLower() switch
        {
            WorkforceCategory.WorkforceFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateWorkforceFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.TeachersFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachersFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.SeniorLeadershipFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateSeniorLeadershipFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.TeachingAssistantsFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachingAssistantsFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.NonClassroomSupportStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateNonClassroomSupportStaffFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.AuxiliaryStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateAuxiliaryStaffFte(d, x.year, parameters.Dimension))),
            WorkforceCategory.WorkforceHeadcount => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateWorkforceHeadcount(d, x.year, parameters.Dimension))),
            WorkforceCategory.TeachersQualified => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachersQualified(d, x.year))),
            _ => throw new ArgumentOutOfRangeException(nameof(parameters.Category))
        };
    }
}