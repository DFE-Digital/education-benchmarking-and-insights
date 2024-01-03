using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

[ExcludeFromCodeCoverage]
public static class SchoolFactory
{
    public static School Create(Edubase edubase)
    {
        return new School
        {
            Urn = edubase.URN.ToString(),
            Kind = edubase.TypeOfEstablishment,
            FinanceType = edubase.FinanceType,
            Name = edubase.EstablishmentName
        };
    }
}