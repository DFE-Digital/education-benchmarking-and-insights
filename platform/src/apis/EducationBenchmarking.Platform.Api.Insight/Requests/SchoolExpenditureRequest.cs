using System;
using EducationBenchmarking.Platform.Api.Insight.Models;

namespace EducationBenchmarking.Platform.Api.Insight.Requests;

public class SchoolExpenditureRequest
{
    public string[] Urns { get; set; } = Array.Empty<string>();
    public SectionDimensions Dimensions { get; set; } = new();
}