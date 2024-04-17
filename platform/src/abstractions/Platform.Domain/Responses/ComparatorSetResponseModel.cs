using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorSetResponseModel
{
    public IEnumerable<string>? DefaultPupil { get; set; }
    public IEnumerable<string>? DefaultArea { get; set; }

    public static ComparatorSetResponseModel Create(IEnumerable<ComparatorDataObject> results)
    {
        var defaultPupil = new List<string>();
        var defaultArea = new List<string>();

        foreach (var result in results)
        {
            var urn = result.UKPRN_URN2.Split("_")[1];
            if (urn == null) continue;

            if (result is { PeerGroup: ComparatorSetTypes.Default, CostGroup: ComparatorSetTypes.Pupil })
            {
                defaultPupil.Add(urn);
            }

            if (result is { PeerGroup: ComparatorSetTypes.Default, CostGroup: ComparatorSetTypes.Area })
            {
                defaultArea.Add(urn);
            }
        }

        return new ComparatorSetResponseModel
        {
            DefaultPupil = defaultPupil,
            DefaultArea = defaultArea
        };
    }
}