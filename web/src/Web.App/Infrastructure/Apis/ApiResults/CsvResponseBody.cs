namespace Web.App.Infrastructure.Apis;

public class CsvResponseBody(byte[] bytes) : ApiResponseBody(bytes);