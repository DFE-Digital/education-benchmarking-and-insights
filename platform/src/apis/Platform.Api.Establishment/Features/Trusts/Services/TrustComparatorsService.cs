using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Trusts.Services;

public interface ITrustComparatorsService
{
    Task<TrustComparators> ComparatorsAsync(string companyNumber, TrustComparatorsRequest request, CancellationToken cancellationToken = default);
}

//TODO : Lookup target trust and add comparators ordering
[ExcludeFromCodeCoverage]
public class TrustComparatorsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.TrustComparators)] IIndexClient client)
    : SearchService<TrustComparator>(client), ITrustComparatorsService
{
    public async Task<TrustComparators> ComparatorsAsync(string companyNumber, TrustComparatorsRequest request, CancellationToken cancellationToken = default)
    {
        //var trust = await connection.LookUpAsync(request.Target);

        var filter = request.FilterExpression(companyNumber);
        var search = request.SearchExpression();
        var result = await SearchWithScoreAsync(search, filter, 100000, cancellationToken);

        return new TrustComparators
        {
            TotalTrusts = result.Total,
            Trusts = result.Response
                //.OrderByDescending(x => CalculateScore(request, x, school))
                .Select(x => x.Document?.CompanyNumber)
                .OfType<string>().Take(9) // Comparator set is 10 (target trust + 9 similar trusts)
        };
    }
}