using EducationBenchmarking.Platform.Shared.Characteristics;

namespace EducationBenchmarking.Platform.Shared;

public class ComparatorSetRequest
{
    public bool IncludeSet { get; set; } = false; 
    public Dictionary<string, Value>? Characteristics { get; set; }
}

public class SchoolComparatorSetRequest : ComparatorSetRequest
{
    
}

public class TrustComparatorSetRequest : ComparatorSetRequest
{
    
}