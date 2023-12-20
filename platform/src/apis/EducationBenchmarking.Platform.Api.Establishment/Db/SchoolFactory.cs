using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

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