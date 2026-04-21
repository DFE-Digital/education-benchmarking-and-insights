using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiPhaseParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiPhaseParameterAttribute() : base("phase")
    {
        Type = typeof(string);
        Required = false;
        Description = "Overall phase for response values";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiFinanceTypeParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiFinanceTypeParameterAttribute() : base("financeType")
    {
        Type = typeof(string);
        Required = false;
        Description = "Finance type for response values";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiIdentifierParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiIdentifierParameterAttribute() : base("identifier")
    {
        Type = typeof(string);
        Required = true;
        Description = "The identifier of the user defined comparator set.";
        In = ParameterLocation.Path;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiCompanyNumberParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiCompanyNumberParameterAttribute() : base("companyNumber")
    {
        Type = typeof(string);
        Required = false;
        Description = "Eight digit trust company number";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiLaCodeParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiLaCodeParameterAttribute() : base("laCode")
    {
        Type = typeof(string);
        Required = false;
        Description = "Local authority three digit code";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiUseCustomDataParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiUseCustomDataParameterAttribute() : base("useCustomData")
    {
        Type = typeof(bool);
        Required = false;
        Description = "Sets whether or not to use custom data context";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiCategoriesParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiCategoriesParameterAttribute() : base("categories")
    {
        Type = typeof(string[]);
        Required = false;
        Description = "List of cost categories";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiStatusesParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiStatusesParameterAttribute() : base("statuses")
    {
        Type = typeof(string[]);
        Required = false;
        Description = "List of RAG statuses";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiOverallPhaseParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiOverallPhaseParameterAttribute() : base("overallPhase")
    {
        Type = typeof(string);
        Required = false;
        Description = "School overall phase";
        In = ParameterLocation.Query;
    }
}

[ExcludeFromCodeCoverage]
public sealed class OpenApiApiVersionParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiApiVersionParameterAttribute() : base(Platform.Functions.Constants.ApiVersion)
    {
        Type = typeof(string);
        Required = false;
        Description = "The requested API version";
        In = ParameterLocation.Header;
    }
}