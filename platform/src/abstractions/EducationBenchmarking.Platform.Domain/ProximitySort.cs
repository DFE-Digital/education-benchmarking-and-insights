using System;
using System.Collections.Generic;
using System.Linq;
using EducationBenchmarking.Platform.Domain.Extensions;

namespace EducationBenchmarking.Platform.Domain;

public static class ProximitySortKinds
{
    public const string Sen = "SEN";
    public const string Bic = "BestInClass";
    public const string Simple = "Simple";
}

public abstract class ProximitySort
{ 
    public abstract string Kind { get; }
    public abstract IEnumerable<SchoolTrustFinance> Sort(IEnumerable<SchoolTrustFinance> set);
}

public class UnknownProximitySort : ProximitySort
{
    public override string Kind => "";
    public override IEnumerable<SchoolTrustFinance> Sort(IEnumerable<SchoolTrustFinance> set)
    {
        throw new NotImplementedException();
    }
}

public class SenProximitySort : ProximitySort
{
    public override string Kind => ProximitySortKinds.Sen;
    public string? SortBy { get; set; }
    public decimal Baseline { get; set; }

    public override IEnumerable<SchoolTrustFinance> Sort(IEnumerable<SchoolTrustFinance> set)
    {
        var enumerable = set as SchoolTrustFinance[] ?? set.ToArray();

        return enumerable
                .OrderBy(x => Math.Abs(x.DecimalValueByName<SchoolTrustFinance>(SortBy).GetValueOrDefault() - Baseline));
    }
}

public class SimpleProximitySort : ProximitySort
{
    public override string Kind => ProximitySortKinds.Simple;
    public string? SortBy { get; set; }
    public decimal Baseline { get; set; }

    public override IEnumerable<SchoolTrustFinance> Sort(IEnumerable<SchoolTrustFinance> set)
    {
        var enumerable = set as SchoolTrustFinance[] ?? set.ToArray();

        return enumerable
                .OrderBy(x => Math.Abs(x.DecimalValueByName<SchoolTrustFinance>(SortBy).GetValueOrDefault() - Baseline));
    }
}

public class BestInClassProximitySort : ProximitySort
{
    public override string Kind => ProximitySortKinds.Bic;
    public string? SortBy { get; set; }
    public decimal Baseline { get; set; }
    public int Pool { get; set; }

    public override IEnumerable<SchoolTrustFinance> Sort(IEnumerable<SchoolTrustFinance> set)
    {
        var enumerable = set as SchoolTrustFinance[] ?? set.ToArray();
        
        return enumerable
                .OrderBy(x => Math.Abs(x.DecimalValueByName<SchoolTrustFinance>(SortBy).GetValueOrDefault() - Baseline))
                .Take(Pool)
                .OrderByDescending(x => x.OverallPhase is "Secondary" or "All-through" ? x.Progress8Measure : x.Ks2Progress);
    }
}