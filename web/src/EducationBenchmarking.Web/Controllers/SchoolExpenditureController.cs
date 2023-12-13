using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers
{
    [Controller]
    [Route("school-expenditure")]
    public class SchoolExpenditureController : Controller
    {
        [Route("{id}")]
        public IActionResult Details(string id)
        {
            var viewModel = new SchoolExpenditureViewModel
            {
                SchoolName = "Three Bears Secondary School",
                LastFinancialYear = "2022"
            };

            return View(viewModel);
        }
    }
}
