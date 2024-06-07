using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;

namespace Platform.Api.Benchmark.Comparators;

public interface IComparatorTrustsService
{
    Task<ComparatorTrusts> ComparatorsAsync(ComparatorTrustsRequest request);
}

[ExcludeFromCodeCoverage]
public class ComparatorTrustsService : SearchService, IComparatorTrustsService
{
    private const string IndexName = SearchResourceNames.Indexes.TrustComparators;

    public ComparatorTrustsService(IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName,
        options.Value.Credential)
    {
    }

    public async Task<ComparatorTrusts> ComparatorsAsync(ComparatorTrustsRequest request)
    {
        var trust = await LookUpAsync<ComparatorTrust>(request.Target);

        var filter = request.FilterExpression();
        var search = request.SearchExpression();
        var result = await SearchAsync<ComparatorTrust>(search, filter, 100000);

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