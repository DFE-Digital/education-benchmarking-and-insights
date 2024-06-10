using System.Text;
using Newtonsoft.Json;
using Web.App.Extensions;
namespace Web.App.Infrastructure.Apis;

public class JsonContent(object value) : StringContent(value.ToJson(Formatting.None), Encoding.UTF8, "application/json");