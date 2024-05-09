namespace Platform.Domain;

public record FloorAreaResponseModel
{

    public string? Urn { get; set; }
    public int? FloorArea { get; set; }

    public static FloorAreaResponseModel? Create(FloorAreaDataObject? dataObject)
    {
        return dataObject == null
            ? null
            : new FloorAreaResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                FloorArea = dataObject.FloorArea
            };
    }
}