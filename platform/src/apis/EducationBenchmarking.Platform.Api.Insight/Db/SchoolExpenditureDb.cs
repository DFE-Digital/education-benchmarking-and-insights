using System.Collections.Generic;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Primitives;

namespace EducationBenchmarking.Platform.Api.School.Db;

public interface ISchoolExpenditureDb
{
    Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria);
}

public class SchoolExpenditureDb : ISchoolExpenditureDb
{
    public async Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria)
    {
        var schools = new SchoolExpenditure[]
        {
            new() { Urn = "140558", Name = "St Joseph's Catholic Primary School, Moorthorpe", Kind ="Academy sponsor led", FinanceType = "Academies",  LocalAuthority = "", TotalExpenditure = 1000000 },
            new() { Urn = "135558", Name = "Hawkswood Primary Pru", Kind ="Pupil referral unit", FinanceType = "Maintained", LocalAuthority = "", TotalExpenditure = 2000000},
            new() { Urn = "105376", Name = "Cloughside College", Kind ="Community special school", FinanceType = "Maintained", LocalAuthority = "", TotalExpenditure = 3000000 },
            new() { Urn = "112858", Name = "Stoney Middleton Cofe (C) Primary School", FinanceType = "Maintained", Kind ="Voluntary controlled school", LocalAuthority = "", TotalExpenditure = 4000000 },
            new() { Urn = "122233", Name = "Kielder Primary School And Nursery", FinanceType = "Maintained", Kind ="Community school", LocalAuthority = "", TotalExpenditure = 5000000 },
            new() { Urn = "118155", Name = "Chillerton And Rookley Primary School", FinanceType = "Maintained", Kind ="Community school", LocalAuthority = "", TotalExpenditure = 6000000 },
            new() { Urn = "112267", Name = "Asby Endowed School", Kind ="Voluntary controlled school", FinanceType = "Maintained", LocalAuthority = "", TotalExpenditure = 7000000 }
        };

        return await Task.FromResult( PagedResults<SchoolExpenditure>.Create(schools));
    }
}