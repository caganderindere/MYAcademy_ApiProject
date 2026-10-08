using Microsoft.AspNetCore.Mvc;

namespace SerieA.API.Controllers
{
    public class MatchGoalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
