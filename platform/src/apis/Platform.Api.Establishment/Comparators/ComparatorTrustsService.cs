using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Search;
namespace Platform.Api.Establishment.Comparators;

public interface IComparatorTrustsService
{
    Task<ComparatorTrusts> ComparatorsAsync(string companyNumber, ComparatorTrustsRequest request);
}

[ExcludeFromCodeCoverage]
public class ComparatorTrustsService(ISearchConnection<ComparatorTrust> connection) : IComparatorTrustsService
{
    public async Task<ComparatorTrusts> ComparatorsAsync(string companyNumber, ComparatorTrustsRequest request)
    {
        //var trust = await connection.LookUpAsync(request.Target);

        var filter = request.FilterExpression(companyNumber);
        var search = request.SearchExpression();
        var result = await connection.SearchAsync(search, filter, 100000);

        return new ComparatorTrusts
        {
            TotalTrusts = result.Total,
            Trusts = result.Response
                //.OrderByDescending(x => CalculateScore(request, x, school))
                .Select(x => x.Document?.CompanyNumber)
                .OfType<string>().Take(9) // Comparator set is 10 (target trust + 9 similar trusts)
        };
    }
}