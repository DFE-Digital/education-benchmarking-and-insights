using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IWorkforceDb
{
    Task<IEnumerable<WorkforceResponseModel>> GetHistory(string urn, WorkforceDimension dimension);
    Task<IEnumerable<WorkforceResponseModel>> Get(string[] urns, string category, WorkforceDimension dimension);
}

public class WorkforceDb : FinancesDb, IWorkforceDb
{
    public WorkforceDb(IOptions<FinancesDbOptions> options, ICosmosClientFactory factory) : base(options.Value, factory)
    {
    }

    public async Task<IEnumerable<WorkforceResponseModel>> GetHistory(string urn, WorkforceDimension dimension)
    {
        var finances = await GetFinancesHistory<WorkforceDataObject>(urn);

        return finances
            .OfType<(int Term, WorkforceDataObject? DataObject)>()
            .Select(x => WorkforceResponseModel.Create(x.DataObject, x.Term, dimension));
    }

    public async Task<IEnumerable<WorkforceResponseModel>> Get(string[] urns, string category, WorkforceDimension dimension)
    {
        var finances = await GetFinances<WorkforceDataObject>(urns);
        return category.ToLower() switch
        {
            WorkforceCategory.WorkforceFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateWorkforceFte(d, x.year, dimension))),
            WorkforceCategory.TeachersFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachersFte(d, x.year, dimension))),
            WorkforceCategory.SeniorLeadershipFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateSeniorLeadershipFte(d, x.year, dimension))),
            WorkforceCategory.TeachingAssistantsFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachingAssistantsFte(d, x.year, dimension))),
            WorkforceCategory.NonClassroomSupportStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateNonClassroomSupportStaffFte(d, x.year, dimension))),
            WorkforceCategory.AuxiliaryStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateAuxiliaryStaffFte(d, x.year, dimension))),
            WorkforceCategory.WorkforceHeadcount => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateWorkforceHeadcount(d, x.year, dimension))),
            WorkforceCategory.TeachersQualified => finances
                .SelectMany(x => x.dataObject
                    .Select(d => WorkforceResponseModel.CreateTeachersQualified(d, x.year))),
            _ => throw new ArgumentOutOfRangeException(nameof(category))
        };
    }
}