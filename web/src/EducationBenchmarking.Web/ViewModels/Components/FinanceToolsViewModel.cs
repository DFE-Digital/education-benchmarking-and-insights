using EducationBenchmarking.Web.Controllers;

namespace EducationBenchmarking.Web.ViewModels.Components;

public class FinanceToolsViewModel(string identifier, IEnumerable<FinanceTools> tools)
{
    public IEnumerable<FinanceTools> Tools => tools;
    public string Identifier => identifier;
}