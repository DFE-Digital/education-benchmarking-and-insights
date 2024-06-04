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
                .OfType<string>().Take(29) // Comparator set is 30 (target school + 29 similar schools)
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

        if (request.TotalPupils != null && x.Document.TotalPupils != null && school.TotalPupils != null)
        {
            baseScore += CalcRatio(x.Document.TotalPupils.Value, school.TotalPupils.Value);
        }

        if (request.AverageBuildingAge != null && x.Document.AverageBuildingAge != null &&
            school.AverageBuildingAge != null)
        {
            baseScore += CalcRatio(x.Document.AverageBuildingAge.Value, school.AverageBuildingAge.Value);
        }

        if (request.TotalInternalFloorArea != null && x.Document.TotalInternalFloorArea != null &&
            school.TotalInternalFloorArea != null)
        {
            baseScore += CalcRatio(x.Document.TotalInternalFloorArea.Value, school.TotalInternalFloorArea.Value);
        }

        if (request.PercentFreeSchoolMeals != null && x.Document.PercentFreeSchoolMeals != null &&
            school.PercentFreeSchoolMeals != null)
        {
            baseScore += CalcRatio(x.Document.PercentFreeSchoolMeals.Value, school.PercentFreeSchoolMeals.Value);
        }

        if (request.PercentSpecialEducationNeeds != null && x.Document.PercentSpecialEducationNeeds != null &&
            school.PercentSpecialEducationNeeds != null)
        {
            baseScore += CalcRatio(x.Document.PercentSpecialEducationNeeds.Value, school.PercentSpecialEducationNeeds.Value);
        }

        if (request.TotalPupilsSixthForm != null && x.Document.TotalPupilsSixthForm != null &&
            school.TotalPupilsSixthForm != null)
        {
            baseScore += CalcRatio(x.Document.TotalPupilsSixthForm.Value, school.TotalPupilsSixthForm.Value);
        }

        if (request.KS2Progress != null && x.Document.KS2Progress != null &&
            school.KS2Progress != null)
        {
            baseScore += CalcRatio(x.Document.KS2Progress.Value, school.KS2Progress.Value);
        }

        if (request.KS4Progress != null && x.Document.KS4Progress != null &&
            school.KS4Progress != null)
        {
            baseScore += CalcRatio(x.Document.KS4Progress.Value, school.KS4Progress.Value);
        }

        if (request.SchoolsInTrust != null && x.Document.SchoolsInTrust != null &&
            school.SchoolsInTrust != null)
        {
            baseScore += CalcRatio(x.Document.SchoolsInTrust.Value, school.SchoolsInTrust.Value);
        }

        if (request.PercentWithVI != null && x.Document.PercentWithVI != null &&
            school.PercentWithVI != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithVI.Value, school.PercentWithVI.Value);
        }

        if (request.PercentWithSPLD != null && x.Document.PercentWithSPLD != null &&
            school.PercentWithSPLD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithSPLD.Value, school.PercentWithSPLD.Value);
        }

        if (request.PercentWithSLD != null && x.Document.PercentWithSLD != null &&
            school.PercentWithSLD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithSLD.Value, school.PercentWithSLD.Value);
        }

        if (request.PercentWithSLCN != null && x.Document.PercentWithSLCN != null &&
            school.PercentWithSLCN != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithSLCN.Value, school.PercentWithSLCN.Value);
        }

        if (request.PercentWithSEMH != null && x.Document.PercentWithSEMH != null &&
            school.PercentWithSEMH != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithSEMH.Value, school.PercentWithSEMH.Value);
        }

        if (request.PercentWithPMLD != null && x.Document.PercentWithPMLD != null &&
            school.PercentWithPMLD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithPMLD.Value, school.PercentWithPMLD.Value);
        }

        if (request.PercentWithPD != null && x.Document.PercentWithPD != null &&
            school.PercentWithPD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithPD.Value, school.PercentWithPD.Value);
        }

        if (request.PercentWithOTH != null && x.Document.PercentWithOTH != null &&
            school.PercentWithOTH != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithOTH.Value, school.PercentWithOTH.Value);
        }

        if (request.PercentWithMSI != null && x.Document.PercentWithMSI != null &&
            school.PercentWithMSI != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithMSI.Value, school.PercentWithMSI.Value);
        }

        if (request.PercentWithMLD != null && x.Document.PercentWithMLD != null &&
            school.PercentWithMLD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithMLD.Value, school.PercentWithMLD.Value);
        }

        if (request.PercentWithHI != null && x.Document.PercentWithHI != null &&
            school.PercentWithHI != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithHI.Value, school.PercentWithHI.Value);
        }

        if (request.PercentWithASD != null && x.Document.PercentWithASD != null &&
            school.PercentWithASD != null)
        {
            baseScore += CalcRatio(x.Document.PercentWithASD.Value, school.PercentWithASD.Value);
        }

        return 1 / baseScore + x.Score;
    }

    private static double CalcRatio(double current, double target)
    {
        return Math.Abs((current - target) / target);
    }
}