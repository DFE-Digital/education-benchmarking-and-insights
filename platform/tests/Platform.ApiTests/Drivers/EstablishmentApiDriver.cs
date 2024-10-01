namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class EstablishmentApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.Establishment, output);