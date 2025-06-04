namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class ChartRenderingApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.ChartRendering, output);