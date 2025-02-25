namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class LocalAuthorityFinancesApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.LocalAuthorityFinances, output);