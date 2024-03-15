using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolHistoryViewModel
{
    public string Name { get; private set; }
    public string Urn { get; private set; }
    public List<SchoolYearHistory> History { get; private set; }
    
    
    public class SchoolYearHistory
    {
        public string Term { get; set; }
        public double InYearBalance { get; set; }
        public double RevenueReserve { get; set; }
        public double TotalExpenditure { get; set; }
        public double TotalIncome { get; set; }
        public int NoPupils { get; set; }
    }

    public SchoolHistoryViewModel(School school, List<SchoolYearHistory> historyData = null)
    {
        Name = school.Name;
        Urn = school.Urn;
        History = new List<SchoolYearHistory>();

        if (historyData != null)
        {
            foreach (var history in historyData)
            {
                History.Add(new SchoolYearHistory
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
}
