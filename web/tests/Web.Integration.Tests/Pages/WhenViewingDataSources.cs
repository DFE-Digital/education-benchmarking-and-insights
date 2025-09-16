using Xunit;
using File = Web.App.Domain.Content.File;

namespace Web.Integration.Tests.Pages;

public class WhenViewingDataSources(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var files = new File[]
        {
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2022/23",
                FileName = "CFR_2022-23_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2021/22",
                FileName = "CFR_2021-22_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2020/21",
                FileName = "CFR_2020-21_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2019/20",
                FileName = "CFR_2019-20_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2018/19",
                FileName = "CFR_2018-19_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2017/18",
                FileName = "CFR_2017-18_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2016/17",
                FileName = "CFR_2016-17_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2015/16",
                FileName = "CFR_2015-16_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-cfr",
                Label = "CFR 2014/15",
                FileName = "CFR_2014-15_Full_Data_Workbook.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2022/23",
                FileName = "AAR_2022-23_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2021/22",
                FileName = "AAR_2021-22_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2020/21",
                FileName = "AAR_2020-21_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2019/20",
                FileName = "AAR_2019-20_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2018/19",
                FileName = "AAR_2018-19_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2017/18",
                FileName = "AAR_2017-18_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2016/17",
                FileName = "AAR_2016-17_download.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2015/16",
                FileName = "SFR32_2017_Main_Tables.xlsx"
            },
            new()
            {
                Type = "transparency-aar",
                Label = "AAR 2014/15",
                FileName = "SFR27_2016_Main_Tables.xlsx"
            }
        };

        var expectedMaintainedSchools = new[]
        {
            new
            {
                DisplayText = "CFR 2022/23",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2022-23_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2021/22",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2021-22_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2020/21",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2020-21_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2019/20",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2019-20_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2018/19",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2018-19_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2017/18",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2017-18_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2016/17",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2016-17_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2015/16",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2015-16_Full_Data_Workbook.xlsx"
            },
            new
            {
                DisplayText = "CFR 2014/15",
                Link = "https://teststorageaccount.net/testcontainer/CFR_2014-15_Full_Data_Workbook.xlsx"
            }
        };

        var expectedAcademies = new[]
        {
            new
            {
                DisplayText = "AAR 2022/23",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2022-23_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2021/22",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2021-22_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2020/21",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2020-21_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2019/20",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2019-20_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2018/19",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2018-19_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2017/18",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2017-18_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2016/17",
                Link = "https://teststorageaccount.net/testcontainer/AAR_2016-17_download.xlsx"
            },
            new
            {
                DisplayText = "AAR 2015/16",
                Link = "https://teststorageaccount.net/testcontainer/SFR32_2017_Main_Tables.xlsx"
            },
            new
            {
                DisplayText = "AAR 2014/15",
                Link = "https://teststorageaccount.net/testcontainer/SFR27_2016_Main_Tables.xlsx"
            }
        };

        var page = await Client
            .SetupWebAssetsOptions("https://teststorageaccount.net/testcontainer")
            .SetupFiles(files)
            .Navigate(Paths.DataSources);

        var laHeading = page.QuerySelectorAll("h4.govuk-heading-s")
            .FirstOrDefault(h => h.TextContent.Contains("LA Maintained Schools"));
        Assert.NotNull(laHeading);

        var laList = laHeading.NextElementSibling;
        Assert.NotNull(laList);

        var laReturns = laList.QuerySelectorAll("li > a.govuk-link");
        Assert.NotNull(laReturns);

        for (var i = 0; i < laReturns.Length; i++)
        {
            var link = laReturns[i];
            Assert.Contains(expectedMaintainedSchools[i].DisplayText, link.TextContent);
            Assert.Equal(expectedMaintainedSchools[i].Link, link.GetAttribute("href"));
        }

        var academiesHeading = page.QuerySelectorAll("h4.govuk-heading-s")
            .FirstOrDefault(h => h.TextContent.Contains("Academies"));
        Assert.NotNull(academiesHeading);

        var academiesList = academiesHeading.NextElementSibling;
        Assert.NotNull(academiesList);

        var academiesReturns = academiesList.QuerySelectorAll("li > a.govuk-link");
        Assert.NotNull(academiesReturns);

        for (var i = 0; i < academiesReturns.Length; i++)
        {
            var link = academiesReturns[i];
            Assert.Contains(expectedAcademies[i].DisplayText, link.TextContent);
            Assert.Contains(expectedAcademies[i].Link, link.GetAttribute("href"));
        }

        DocumentAssert.AssertPageUrl(page, Paths.DataSources.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.DataSources.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Data sources and interpretation");
    }
}