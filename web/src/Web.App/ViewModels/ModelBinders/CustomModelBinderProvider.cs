using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.App.ViewModels.ModelBinders;

[ExcludeFromCodeCoverage]
public class CustomModelBinderProvider(IModelBinderProvider rootProvider) : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        IModelBinder? rootBinder;
        Type binderType;

        var typeName = context.Metadata.ModelType.Name;
        switch (typeName)
        {
            case nameof(WorkforceDataCustomDataViewModel):
                rootBinder = rootProvider.GetBinder(context);
                binderType = typeof(WorkforceDataCustomDataViewModelBinder);
                break;
            default:
                return null;
        }

        if (rootBinder == null)
        {
            throw new InvalidOperationException($"Root {rootProvider.GetType()} did not provide an {nameof(IModelBinder)} for {typeName}.");
        }

        // wrap specific model binder in another binder that resolves dependency injected services
        return new ModelBinderWithRequestServices(binderType, rootBinder);
    }
}