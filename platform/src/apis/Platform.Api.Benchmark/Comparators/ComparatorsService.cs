using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;

namespace Platform.Api.Benchmark.Comparators;

public interface IComparatorsService
{
    Task<ComparatorSchools> SchoolsAsync(ComparatorSchoolsRequest request);
}

[ExcludeFromCodeCoverage]
public class ComparatorsService : SearchService, IComparatorsService
{
    private const string IndexName = SearchResourceNames.Indexes.SchoolComparators;

    public ComparatorsService(IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName,
        options.Value.Credential)
    {
    }

    public async Task<ComparatorSchools> SchoolsAsync(ComparatorSchoolsRequest request)
    {
        var school = await LookUpAsync<ComparatorSchool>(request.Target);

        var filter = request.FilterExpression();
        var search = request.SearchExpression();
        var result = await SearchAsync<ComparatorSchool>(search, filter, 100000);

        return new ComparatorSchools
        {
            TotalSchools = result.Total,
            Schools = result.Response
                .OrderByDescending(x => CalculateScore(request, x, school))
                .Select(x => x.Document?.URN)
                .OfType<string>().Take(30)
        };
    }

    private static double? CalculateScore(ComparatorSchoolsRequest request,
        ScoreResponse<ComparatorSchool> x,
        ComparatorSchool school)
    {
        if (x.Document == null)
        {
            return x.Score;
        }

        //TODO: Add base distance calculation

        double baseScore = 0;

        if (request.NumberOfPupils != null && x.Document.NumberOfPupils != null && school.NumberOfPupils != null)
        {
            baseScore += CalcRatio(x.Document.NumberOfPupils.Value, school.NumberOfPupils.Value);
        }

        if (request.AverageBuildingAge != null && x.Document.AverageBuildingAge != null &&
            school.AverageBuildingAge != null)
        {
            baseScore += CalcRatio(x.Document.AverageBuildingAge.Value, school.AverageBuildingAge.Value);
        }

        if (request.GrossInternalFloorArea != null && x.Document.GrossInternalFloorArea != null &&
            school.GrossInternalFloorArea != null)
        {
            baseScore += CalcRatio(x.Document.GrossInternalFloorArea.Value, school.GrossInternalFloorArea.Value);
        }

        if (request.PercentFreeSchoolMeals != null && x.Document.PercentFreeSchoolMeals != null &&
            school.PercentFreeSchoolMeals != null)
        {
            baseScore += CalcRatio(x.Document.PercentFreeSchoolMeals.Value, school.PercentFreeSchoolMeals.Value);
        }

        if (request.PercentSenWithoutPlan != null && x.Document.PercentSenWithoutPlan != null &&
            school.PercentSenWithoutPlan != null)
        {
            baseScore += CalcRatio(x.Document.PercentSenWithoutPlan.Value, school.PercentSenWithoutPlan.Value);
        }

        if (request.PercentSenWithPlan != null && x.Document.PercentSenWithPlan != null &&
            school.PercentSenWithPlan != null)
        {
            baseScore += CalcRatio(x.Document.PercentSenWithPlan.Value, school.PercentSenWithPlan.Value);
        }

        if (request.NumberOfPupilsSixthForm != null && x.Document.NumberOfPupilsSixthForm != null &&
            school.NumberOfPupilsSixthForm != null)
        {
            baseScore += CalcRatio(x.Document.NumberOfPupilsSixthForm.Value, school.NumberOfPupilsSixthForm.Value);
        }

        if (request.KeyStage2Progress != null && x.Document.KeyStage2Progress != null &&
            school.KeyStage2Progress != null)
        {
            baseScore += CalcRatio(x.Document.KeyStage2Progress.Value, school.KeyStage2Progress.Value);
        }

        if (request.KeyStage4Progress != null && x.Document.KeyStage4Progress != null &&
            school.KeyStage4Progress != null)
        {
            baseScore += CalcRatio(x.Document.KeyStage4Progress.Value, school.KeyStage4Progress.Value);
        }

        if (request.NumberSchoolsInTrust != null && x.Document.NumberSchoolsInTrust != null &&
            school.NumberSchoolsInTrust != null)
        {
            baseScore += CalcRatio(x.Document.NumberSchoolsInTrust.Value, school.NumberSchoolsInTrust.Value);
        }

        return 1 / baseScore + x.Score;
    }

    private static double CalcRatio(double current, double target)
    {
        return Math.Abs((current - target) / target);
    }
}