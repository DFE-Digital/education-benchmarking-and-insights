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

            //    var query = HttpContext.Request.QueryString.Value;

            //    var viewModel = new SchoolExpenditureViewModel
            //    {
            //        QueryString = string.IsNullOrEmpty(query) ? "" : query.Substring(1)
            //};

            return View(viewModel);
        }
    }
}
