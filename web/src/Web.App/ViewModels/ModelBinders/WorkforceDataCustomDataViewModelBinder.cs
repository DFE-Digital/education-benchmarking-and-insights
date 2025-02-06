using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Web.App.Domain;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.ViewModels.ModelBinders;

// Run default model binding, then add customisation (https://stackoverflow.com/a/73505409/504477)
[ExcludeFromCodeCoverage]
public class WorkforceDataCustomDataViewModelBinder(
    IModelBinder defaultBinder,
    IModelMetadataProvider metadataProvider,
    ICensusApi censusApi,
    ILoggerFactory loggerFactory) : IModelBinder
{
    private readonly ILogger<WorkforceDataCustomDataViewModelBinder> _logger = loggerFactory.CreateLogger<WorkforceDataCustomDataViewModelBinder>();

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        _logger.LogDebug("Executing default binder");
        await defaultBinder.BindModelAsync(bindingContext);

        if (bindingContext.Model is not WorkforceDataCustomDataViewModel { HasValues: true } model)
        {
            return;
        }

        var urn = bindingContext.ModelState["urn"]?.AttemptedValue;
        if (string.IsNullOrWhiteSpace(urn))
        {
            return;
        }

        // AB#246376: backfill missing dependent fields used for validation
        _logger.LogDebug("Retrieving current census data for {URN}", urn);
        var census = await censusApi.Get(urn).GetResultOrDefault<Census>();
        model.WorkforceFte ??= census?.Workforce;
        model.TeachersFte ??= census?.Teachers;

        // Re-run data annotation validation (https://github.com/dotnet/aspnetcore/issues/18924)
        _logger.LogDebug("Adding updated model to validation state");
        bindingContext.ValidationState.Add(model, new ValidationStateEntry
        {
            Key = bindingContext.ModelName,
            Metadata = metadataProvider.GetMetadataForType(typeof(WorkforceDataCustomDataViewModel))
        });

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}