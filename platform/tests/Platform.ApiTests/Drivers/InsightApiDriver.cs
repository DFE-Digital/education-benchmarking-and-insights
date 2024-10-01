namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class InsightApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.Insight, output);