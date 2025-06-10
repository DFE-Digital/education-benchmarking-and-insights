namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class ContentApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.Content, output);