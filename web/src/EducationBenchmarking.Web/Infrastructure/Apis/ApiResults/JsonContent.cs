using System.Text;
using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class JsonContent(object value) : StringContent(value.ToJson(), Encoding.UTF8, "application/json");