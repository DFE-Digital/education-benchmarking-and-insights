using EducationBenchmarking.Web.Domain;
using System.Collections.Generic;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolHistoryViewModel
{
    public string Name { get; private set; }
    public string Urn { get; private set; }
    public List<SchoolYearHistoryViewModel> History { get; private set; }

    public SchoolHistoryViewModel(School school, List<SchoolYearHistory> historyData)
    {
        Name = school.Name;
        Urn = school.Urn;
        History = new List<SchoolYearHistoryViewModel>();

        foreach (var history in historyData)
        {
            History.Add(new SchoolYearHistoryViewModel
            {
                Term = history.Term,
                InYearBalance = history.InYearBalance,
                RevenueReserve = history.RevenueReserve,
                TotalExpenditure = history.TotalExpenditure,
                TotalIncome = history.TotalIncome,
                NoPupils = history.NoPupils
            });
        }
    }
}

public class SchoolYearHistoryViewModel
{
    public string Term { get; set; }
    public double InYearBalance { get; set; }
    public double RevenueReserve { get; set; }
    public double TotalExpenditure { get; set; }
    public double TotalIncome { get; set; }
    public int NoPupils { get; set; }
}