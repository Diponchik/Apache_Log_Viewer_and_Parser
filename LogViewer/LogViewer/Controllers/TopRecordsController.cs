using Microsoft.AspNetCore.Mvc;

namespace LogViewer.Controllers
{
    public class TopRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
