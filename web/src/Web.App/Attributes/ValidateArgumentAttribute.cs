using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Validators;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class ValidateArgumentAttribute : TypeFilterAttribute
{
    protected ValidateArgumentAttribute(string argumentName, string type) : this(argumentName, type, string.Empty)
    {
    }

    protected ValidateArgumentAttribute(string argumentName, string type, string typeName) : base(typeof(ValidateArgumentFilter))
    {
        ArgumentName = argumentName;
        Type = type;
        TypeName = typeName;

        // arguments list must match the `ValidateArgument` constructor arguments
        Arguments =
        [
            ArgumentName,
            Type,
            TypeName
        ];
    }

    internal string ArgumentName { get; }
    internal string Type { get; }
    internal string TypeName { get; }
}

internal class ValidateArgumentFilter(
    ILogger<ValidateArgumentFilter> logger,
    IValidator<OrganisationIdentifier> validator,
    string argumentName,
    string type,
    string typeName) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue(argumentName, out var identifierValue))
        {
            var parsed = identifierValue?.ToString();

            // Only attempt to validate if non-null or empty value. This _should_ always be the case,
            // otherwise ASP.NET action binding would fail and a `404` would be served by ASP.NET anyway.
            if (!string.IsNullOrWhiteSpace(parsed))
            {
                var parsedType = type;
                if (string.IsNullOrWhiteSpace(parsedType) && !string.IsNullOrWhiteSpace(typeName))
                {
                    if (context.ActionArguments.TryGetValue(typeName, out var typeValue))
                    {
                        parsedType = typeValue?.ToString();
                    }
                }

                // no type provided, so unable to validate identifier
                if (string.IsNullOrWhiteSpace(parsedType))
                {
                    logger.LogDebug("Unable to validate identifier {Identifier} because organisation type could not be resolved", parsed);
                    context.Result = new NotFoundResult();
                }
                else
                {
                    var identifier = new OrganisationIdentifier
                    {
                        Value = parsed,
                        Type = parsedType
                    };

                    var result = validator.Validate(identifier);

                    logger.LogDebug("Validation of identifier {Identifier} of type {Type} returned {ErrorCount} error(s)", identifier.Value, identifier.Type, result.Errors.Count);
                    if (!result.IsValid)
                    {
                        context.Result = new NotFoundResult();
                    }
                }
            }
        }

        base.OnActionExecuting(context);
    }
}

public class ValidateUrnAttribute() : ValidateArgumentAttribute("urn", OrganisationTypes.School);

public class ValidateCompanyNumberAttribute() : ValidateArgumentAttribute("companyNumber", OrganisationTypes.Trust);

public class ValidateLaCodeAttribute() : ValidateArgumentAttribute("code", OrganisationTypes.LocalAuthority);