using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.App.Extensions;

namespace Web.App.Middleware.ModelState;

// https://speednetsoftware.com/post-redirect-get-in-asp-net-razor-pages
public static class ModelStateSerialiser
{
    public static string Serialise(ModelStateDictionary modelState)
    {
        var errorList = modelState
            .Select(kvp => new ModelStateTransferValue
            {
                Key = kvp.Key,
                AttemptedValue = kvp.Value?.AttemptedValue,
                RawValue = kvp.Value?.RawValue,
                ErrorMessages = kvp.Value?.Errors.Select(err => err.ErrorMessage).ToList() ?? []
            });

        return errorList.ToJson();
    }

    public static ModelStateDictionary Deserialise(string serialisedErrorList)
    {
        var modelState = new ModelStateDictionary();
        var errorList = serialisedErrorList.FromJson<List<ModelStateTransferValue>>();
        if (errorList == null)
        {
            return modelState;
        }

        foreach (var item in errorList.Where(e => e.Key != null))
        {
            modelState.SetModelValue(item.Key!, item.RawValue, item.AttemptedValue);
            foreach (var error in item.ErrorMessages)
            {
                modelState.AddModelError(item.Key!, error);
            }
        }

        return modelState;
    }

    private class ModelStateTransferValue
    {
        public string? Key { get; init; }
        public string? AttemptedValue { get; init; }
        public object? RawValue { get; init; }
        public ICollection<string> ErrorMessages { get; init; } = new List<string>();
    }
}