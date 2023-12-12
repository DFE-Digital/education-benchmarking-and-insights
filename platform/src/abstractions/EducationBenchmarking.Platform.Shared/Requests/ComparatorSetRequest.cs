namespace EducationBenchmarking.Platform.Shared;

public class ComparatorSetRequest
{
    public bool IncludeSet { get; set; } = false; 
    public int Size { get; set; }
    public Dictionary<string, CharacteristicValue>? Characteristics { get; set; }
    public ProximitySort? SortMethod { get; set; }
}

public class SchoolComparatorSetRequest : ComparatorSetRequest
{
    
}

public class TrustComparatorSetRequest : ComparatorSetRequest
{
    
}