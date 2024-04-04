namespace Platform.Domain;

public record RatingResponseModel
{
    public string? Urn { get; private set; }
    public int CostCategoryId { get; private set; }
    public string? CostCategory { get; private set; }
    public decimal Value { get; private set; }
    public decimal Median { get; private set; }
    public int Decile { get; private set; }
    public string? Status { get; private set; }
    public int StatusOrder { get; private set; }

    public static RatingResponseModel Create(RatingsDataObject dataObject)
    {
        return new RatingResponseModel
        {
            Urn = dataObject.URN,
            CostCategoryId = dataObject.CostPoolID,
            CostCategory = CostCategories.Mapping[dataObject.CostPoolID],
            Value = dataObject.PerUnit,
            Median = dataObject.MidDecile,
            Decile = dataObject.Decile,
            Status = dataObject.RAGWord,
            StatusOrder = dataObject.RAGOrder
        };
    }
}