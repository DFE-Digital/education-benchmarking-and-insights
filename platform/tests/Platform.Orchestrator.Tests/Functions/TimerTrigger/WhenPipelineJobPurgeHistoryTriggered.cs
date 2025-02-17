using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.TimerTrigger;

public class WhenPipelineJobPurgeHistoryTriggered
{
    private readonly Mock<FunctionContext> _context;
    private readonly TimerInfo _timer;

    public WhenPipelineJobPurgeHistoryTriggered(ITestOutputHelper testOutputHelper)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();

        _context = new Mock<FunctionContext>();
        _context.SetupProperty(functionContext => functionContext.InstanceServices, serviceCollection.BuildServiceProvider());

        _timer = new TimerInfo();
    }

    [Fact]
    public async Task ShouldPurgeAllInstances()
    {
        PurgeInstancesFilter? actualFilter = null;

        var client = new Mock<DurableTaskClient>("name");
        client
            .Setup(c => c.PurgeAllInstancesAsync(It.IsAny<PurgeInstancesFilter>(), It.IsAny<PurgeInstanceOptions?>(), It.IsAny<CancellationToken>()))
            .Callback<PurgeInstancesFilter, PurgeInstanceOptions?, CancellationToken>((filter, _, _) =>
            {
                actualFilter = filter;
            });

        await TimerTriggerFunctions.PipelineJobPurgeHistory(client.Object, _timer, _context.Object);

        client.Verify();
        Assert.NotNull(actualFilter);
        Assert.Equal(DateTime.MinValue, actualFilter.CreatedFrom);
        Assert.InRange((DateTime)actualFilter.CreatedTo?.DateTime!, DateTime.UtcNow.AddDays(-7).AddSeconds(-1), DateTime.UtcNow.AddDays(-7).AddSeconds(1));
        Assert.Contains(OrchestrationRuntimeStatus.Completed, actualFilter.Statuses!);
        Assert.Contains(OrchestrationRuntimeStatus.Failed, actualFilter.Statuses!);
        Assert.Contains(OrchestrationRuntimeStatus.Terminated, actualFilter.Statuses!);
        Assert.Contains(OrchestrationRuntimeStatus.Suspended, actualFilter.Statuses!);
    }

    [Fact]
    public void ShouldSetCronJobToDailyAtNoon()
    {
        var method = typeof(TimerTriggerFunctions).GetMethod(nameof(TimerTriggerFunctions.PipelineJobPurgeHistory));
        var parameters = method?.GetParameters();
        var messageParam = parameters?.SingleOrDefault(p => p.Name == "timer");
        var attribute = messageParam?.GetCustomAttributes(typeof(TimerTriggerAttribute), false).FirstOrDefault() as TimerTriggerAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("0 0 12 * * *", attribute.Schedule);
    }
}