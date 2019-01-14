using Microsoft.AspNetCore.Mvc;

namespace LogViewer.Controllers
{
    public class LogsController : Controller
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
