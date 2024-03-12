namespace Web.App.ViewModels.Components
{
    public class FinanceToolsViewModel(string identifier, IEnumerable<FinanceTools> tools)
    {
        public IEnumerable<FinanceTools> Tools => tools;
        public string Identifier => identifier;
    }
}