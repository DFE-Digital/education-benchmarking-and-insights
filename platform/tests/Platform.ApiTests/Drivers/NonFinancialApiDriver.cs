namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class NonFinancialApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.NonFinancial, output);