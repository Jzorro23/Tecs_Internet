using System.Diagnostics;
using ExerciseAPIs.APIServices;
using ExerciseAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ExerciseAPIs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AutorAsync()
        {
            List<Autores> autores = new List<Autores>();
            autores = await new APIAutores().getAutores();
            return View(autores);
        }

        public async Task<IActionResult> AutorSearch(string name)
        {
            List<Autores> autores = new List<Autores>();
            autores = await new APIAutores().getAutoresByName(name);
            return PartialView("_AutorTable", autores);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
