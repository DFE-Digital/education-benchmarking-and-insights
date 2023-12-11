using System.Collections.Generic;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Primitives;

namespace EducationBenchmarking.Platform.Api.School.Db;

/// <summary>
/// 
/// </summary>
public interface ISchoolExpenditureDb
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria);
}

/// <inheritdoc />
public class SchoolExpenditureDb : ISchoolExpenditureDb
{
    public async Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria)
    {
        var schools = new SchoolExpenditure[]
        {
            new() { Urn = "140558", SchoolName = "St Joseph's Catholic Primary School, Moorthorpe", Kind ="Academy sponsor led", LocalAuthority = "", TotalExpenditure = 1000000 },
            new() { Urn = "135558", SchoolName = "Hawkswood Primary Pru", Kind ="Pupil referral unit", LocalAuthority = "", TotalExpenditure = 2000000},
            new() { Urn = "105376", SchoolName = "Cloughside College", Kind ="Community special school", LocalAuthority = "", TotalExpenditure = 3000000 },
            new() { Urn = "112858", SchoolName = "Stoney Middleton Cofe (C) Primary School", Kind ="Voluntary controlled school", LocalAuthority = "", TotalExpenditure = 4000000 },
            new() { Urn = "122233", SchoolName = "Kielder Primary School And Nursery", Kind ="Community school", LocalAuthority = "", TotalExpenditure = 5000000 },
            new() { Urn = "118155", SchoolName = "Chillerton And Rookley Primary School", Kind ="Community school", LocalAuthority = "", TotalExpenditure = 6000000 },
            new() { Urn = "112267", SchoolName = "Asby Endowed School", Kind ="Voluntary controlled school", LocalAuthority = "", TotalExpenditure = 7000000 }
        };

        return await Task.FromResult( PagedResults<SchoolExpenditure>.Create(schools));
    }
}