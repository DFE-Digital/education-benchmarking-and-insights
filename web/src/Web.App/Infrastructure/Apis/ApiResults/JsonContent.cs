using System.Text;
using Web.App.Extensions;

namespace Web.App.Infrastructure.Apis;

public class JsonContent(object value) : StringContent(value.ToJson(), Encoding.UTF8, "application/json");