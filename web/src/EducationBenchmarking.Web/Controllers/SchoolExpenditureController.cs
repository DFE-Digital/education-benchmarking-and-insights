using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers
{
    public class SchoolExpenditureController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new SchoolExpenditureViewModel
            {
                SchoolName = "MySchool",
                LastFinancialYear = "2022"
            };

            return View(viewModel);
        }
    }
}
