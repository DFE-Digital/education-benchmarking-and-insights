using System.Text;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenAPaginationViewModel
{
    [Theory]
    [InlineData(100, 1, 10, "[1] 2 ... 10 >")]
    [InlineData(100, 2, 10, "< 1 [2] 3 ... 10 >")]
    [InlineData(100, 3, 10, "< 1 2 [3] 4 ... 10 >")]
    [InlineData(100, 4, 10, "< 1 ... 3 [4] 5 ... 10 >")]
    [InlineData(100, 5, 10, "< 1 ... 4 [5] 6 ... 10 >")]
    [InlineData(100, 6, 10, "< 1 ... 5 [6] 7 ... 10 >")]
    [InlineData(100, 7, 10, "< 1 ... 6 [7] 8 ... 10 >")]
    [InlineData(100, 8, 10, "< 1 ... 7 [8] 9 10 >")]
    [InlineData(100, 9, 10, "< 1 ... 8 [9] 10 >")]
    [InlineData(100, 10, 10, "< 1 ... 9 [10]")]
    [InlineData(101, 1, 10, "[1] 2 ... 11 >")]
    [InlineData(11, 1, 10, "[1] 2 >")]
    [InlineData(11, 2, 10, "< 1 [2]")]
    [InlineData(10, 1, 10, "")]
    public void ShouldAllowRenderingOfPagination(int totalResults, int pageNumber, int pageSize, string expected)
    {
        var model = new PaginationViewModel(totalResults, pageNumber, pageSize, _ => null);

        var actual = new StringBuilder();
        if (!model.Visible)
        {
            Assert.Equal(expected, actual.ToString());
            return;
        }

        if (model.HasPreviousPage)
        {
            actual.Append("< ");
        }

        actual.Append(model.CurrentPage == model.FirstPage ? "[1] " : "1 ");

        if (model.SkipBeforeMidPages)
        {
            actual.Append("... ");
        }

        foreach (var i in model.MidPages)
        {
            actual.Append(model.CurrentPage == i ? $"[{i}] " : $"{i} ");
        }

        if (model.SkipAfterMidPages)
        {
            actual.Append("... ");
        }

        if (model.LastPage > 1)
        {
            actual.Append(model.CurrentPage == model.LastPage ? $"[{model.LastPage}]" : model.LastPage);
        }

        if (model.HasNextPage)
        {
            actual.Append(" >");
        }

        Assert.Equal(expected, actual.ToString());
    }
}