using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ICensusDb
{
    Task<IEnumerable<CensusResponseModel>> GetHistory(string urn, CensusDimension dimension);
    Task<IEnumerable<CensusResponseModel>> Get(string[] urns, string category, CensusDimension dimension);
}

public class CensusDb : FinancesDb, ICensusDb
{
    public CensusDb(IOptions<FinancesDbOptions> options, ICosmosClientFactory factory) : base(options.Value, factory)
    {
    }

    public async Task<IEnumerable<CensusResponseModel>> GetHistory(string urn, CensusDimension dimension)
    {
        var finances = await GetFinancesHistory<CensusDataObject>(urn);

        return finances
            .OfType<(int Term, CensusDataObject? DataObject)>()
            .Select(x => CensusResponseModel.Create(x.DataObject, x.Term, dimension));
    }

    public async Task<IEnumerable<CensusResponseModel>> Get(string[] urns, string category, CensusDimension dimension)
    {
        var finances = await GetFinances<CensusDataObject>(urns);
        return category.ToLower() switch
        {
            CensusCategory.WorkforceFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateWorkforceFte(d, x.year, dimension))),
            CensusCategory.TeachersFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateTeachersFte(d, x.year, dimension))),
            CensusCategory.SeniorLeadershipFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateSeniorLeadershipFte(d, x.year, dimension))),
            CensusCategory.TeachingAssistantsFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateTeachingAssistantsFte(d, x.year, dimension))),
            CensusCategory.NonClassroomSupportStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateNonClassroomSupportStaffFte(d, x.year, dimension))),
            CensusCategory.AuxiliaryStaffFte => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateAuxiliaryStaffFte(d, x.year, dimension))),
            CensusCategory.WorkforceHeadcount => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateWorkforceHeadcount(d, x.year, dimension))),
            CensusCategory.TeachersQualified => finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.CreateTeachersQualified(d, x.year))),
            _ => throw new ArgumentOutOfRangeException(nameof(category))
        };
    }
}