namespace Web.App.ViewModels.Components;

public class PaginationViewModel(int totalResults, int pageNumber, int pageSize, Func<int, string?> urlBuilder)
{
    public bool Visible => totalResults > pageSize;
    public int CurrentPage => pageNumber;
    public int FirstPage => 1;
    public int LastPage => Convert.ToInt32(Math.Ceiling(totalResults * 1m / pageSize * 1m));
    public int[] MidPages
    {
        get
        {
            var pages = new List<int>();
            for (var i = CurrentPage - 1; i < CurrentPage + 2; i++)
            {
                if (i > FirstPage && i < LastPage)
                {
                    pages.Add(i);
                }
            }

            return pages.ToArray();
        }
    }
    public bool SkipBeforeMidPages => MidPages.FirstOrDefault(FirstPage) > FirstPage + 1;
    public bool SkipAfterMidPages => MidPages.LastOrDefault(LastPage) < LastPage - 1;
    public bool HasPreviousPage => CurrentPage > FirstPage;
    public bool HasNextPage => CurrentPage * pageSize < totalResults;
    public Func<int, string?> BuildPageUrl => urlBuilder;
}