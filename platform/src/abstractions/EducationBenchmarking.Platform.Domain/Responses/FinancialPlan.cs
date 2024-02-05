using System;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

public class FinancialPlan
{
    public int Year { get; set; }
    public string Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public string CreatedBy { get; set; }
    public int Version { get; set; }
    
    public static FinancialPlan Create(FinancialPlanDataObject dataObject)
    {
        return new FinancialPlan
        {
            Year = int.Parse(dataObject.Id),
            Urn = dataObject.PartitionKey,
            Created = dataObject.Created,
            UpdatedAt = dataObject.UpdatedAt,
            UpdatedBy = dataObject.UpdatedBy,
            CreatedBy = dataObject.CreatedBy,
            Version = dataObject.Version
            
        };
    }
}