using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Domain;

namespace Platform.Api.Insight.Features.ItSpend.Services;

public interface IItSpendService
{
    Task<IEnumerable<ItSpendSchoolResponse>> GetSchoolsAsync(string[] urns, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class ItSpendService() : IItSpendService
{
    // this is purely to stub out data for development purposes - this method will be replaced
    // once db changes are made to facilitate consuming the "real" data
    public Task<IEnumerable<ItSpendSchoolResponse>> GetSchoolsAsync(string[] urns, string dimension,
        CancellationToken cancellationToken = default)
    {
        var models = urns.Select(urn => GetStubData(urn, dimension));
        return Task.FromResult(models);
    }

    private static ItSpendSchoolResponse GetStubData(string urn, string dimension)
    {
        if (!int.TryParse(urn, out var parsedUrn))
        {
            throw new ArgumentException("URN must be numeric", nameof(urn));
        }

        var basePercent = parsedUrn % 100;

        var model = new ItSpendSchoolResponse
        {
            URN = urn,
            SchoolName = $"Stub School ({urn})",
            SchoolType = "Academy",
            LAName = "Test LA",
            PeriodCoveredByReturn = 12,
            TotalPupils = 550
        };

        switch (dimension)
        {
            case Dimensions.Finance.Actuals:
                model.Connectivity = 1.5m * parsedUrn;
                model.OnsiteServers = 2.5m * parsedUrn;
                model.ItLearningResources = 1.8m * parsedUrn;
                model.AdministrationSoftwareAndSystems = 8.5m * parsedUrn;
                model.LaptopsDesktopsAndTablets = 4m * parsedUrn;
                model.OtherHardware = 1.25m * parsedUrn;
                model.ItSupport = 22.2m * parsedUrn;
                break;

            case Dimensions.Finance.PerUnit:
                model.Connectivity = 0.15m * parsedUrn;
                model.OnsiteServers = .25m * parsedUrn;
                model.ItLearningResources = .18m * parsedUrn;
                model.AdministrationSoftwareAndSystems = .09m * parsedUrn;
                model.LaptopsDesktopsAndTablets = 0.3m * parsedUrn;
                model.OtherHardware = 0.1m * parsedUrn;
                model.ItSupport = 0.18m * parsedUrn;
                break;

            case Dimensions.Finance.PercentExpenditure:
                model.Connectivity = 0.015m * basePercent;
                model.OnsiteServers = 0.02m * basePercent;
                model.ItLearningResources = 0.012m * basePercent;
                model.AdministrationSoftwareAndSystems = 0.05m * basePercent;
                model.LaptopsDesktopsAndTablets = 0.2m * basePercent;
                model.OtherHardware = 0.07m * basePercent;
                model.ItSupport = 0.09m * basePercent;
                break;

            case Dimensions.Finance.PercentIncome:
                model.Connectivity = 0.09m * basePercent;
                model.OnsiteServers = 0.014m * basePercent;
                model.ItLearningResources = 0.10m * basePercent;
                model.AdministrationSoftwareAndSystems = 0.04m * basePercent;
                model.LaptopsDesktopsAndTablets = 0.018m * basePercent;
                model.OtherHardware = 0.06m * basePercent;
                model.ItSupport = 0.09m * basePercent;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension");
        }

        return model;
    }
}