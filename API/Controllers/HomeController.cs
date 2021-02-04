using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(){}
        public ViewResult Index()
        {
            return View();
        }
    }
}
