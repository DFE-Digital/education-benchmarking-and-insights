namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PdfResponseBody(byte[] content) : ApiResponseBody(content);