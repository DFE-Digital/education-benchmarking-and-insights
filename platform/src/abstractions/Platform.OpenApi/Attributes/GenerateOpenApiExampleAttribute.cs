namespace Platform.OpenApi.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GenerateOpenApiExampleAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public Type? SourceType { get; set; }
    public string SourceProperty { get; set; } = string.Empty;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GenerateOpenApiPropertiesExampleAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public Type? SourceType { get; set; }
    public string[] Properties { get; set; } = [];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GenerateOpenApiStringValuesExampleAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public string[] Labels { get; set; } = [];
    public string[] Values { get; set; } = [];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GenerateOpenApiIntValuesExampleAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public string[] Labels { get; set; } = [];
    public int[] Values { get; set; } = [];
}
