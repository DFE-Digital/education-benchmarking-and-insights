using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Comparators.Models;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Trust.Features.Comparators.Services;

public interface IComparatorsService
{
    Task<ComparatorsResponse> ComparatorsAsync(string companyNumber, ComparatorsRequest request, CancellationToken cancellationToken = default);
}

//TODO : Lookup target trust and add comparators ordering
[ExcludeFromCodeCoverage]
public class ComparatorsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.TrustComparators)] IIndexClient client)
    : SearchService<ComparatorDto>(client), IComparatorsService
{
    public async Task<ComparatorsResponse> ComparatorsAsync(string companyNumber, ComparatorsRequest request, CancellationToken cancellationToken = default)
    {
        //var trust = await connection.LookUpAsync(request.Target);

        var filter = request.FilterExpression(companyNumber);
        var search = request.SearchExpression();
        var result = await SearchWithScoreAsync(search, filter, 100000, cancellationToken);

        return new ComparatorsResponse
        {
            TotalTrusts = result.Total,
            Trusts = result.Response
                //.OrderByDescending(x => CalculateScore(request, x, school))
                .Select(x => x.Document?.CompanyNumber)
                .OfType<string>().Take(9) // Comparator set is 10 (target trust + 9 similar trusts)
        };
    }
}