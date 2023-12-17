namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class SuggestOutput<T>
{
    public IEnumerable<SuggestValue<T>> Results { get; set; } = Array.Empty<SuggestValue<T>>();
}

public class SuggestValue<T>
{
    public string Text {get; set;}
    public T Document {get; set;}
}