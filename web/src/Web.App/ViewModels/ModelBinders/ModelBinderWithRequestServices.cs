using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.App.ViewModels.ModelBinders;

[ExcludeFromCodeCoverage]
internal class ModelBinderWithRequestServices(IModelBinder rootBinder) : IModelBinder
{
    private readonly ObjectFactory? _factory;

    public ModelBinderWithRequestServices(Type binderType, IModelBinder rootBinder) : this(rootBinder)
    {
        if (!typeof(IModelBinder).IsAssignableFrom(binderType))
        {
            throw new ArgumentException($"Binder type must derive from {nameof(IModelBinder)}.", nameof(binderType));
        }

        _factory = ActivatorUtilities.CreateFactory(binderType, [typeof(IModelBinder)]);
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var requestServices = bindingContext.HttpContext.RequestServices;
        if (_factory?.Invoke(requestServices, [rootBinder]) is IModelBinder binder)
        {
            await binder.BindModelAsync(bindingContext);
        }
    }
}