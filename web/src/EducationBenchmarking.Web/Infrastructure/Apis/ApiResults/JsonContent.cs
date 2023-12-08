using System.Text;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class JsonContent : StringContent
{
    public JsonContent(object value)
        : base(value.ToJson(), Encoding.UTF8, "application/json")
    {}
}