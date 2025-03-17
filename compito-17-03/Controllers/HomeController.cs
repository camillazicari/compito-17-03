using System.Diagnostics;
using compito_17_03.Models;
using Microsoft.AspNetCore.Mvc;
using compito_17_03.Services;
using compito_17_03.ViewModels;

namespace compito_17_03.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentiService _studentiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, StudentiService studentiService)
        {
            _logger = logger;
            _studentiService = studentiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("studenti/get-all")]
        public async Task<IActionResult> StudentiList()
        {
            var studenti = await _studentiService.GetAllStudentiAsync();

            return PartialView("_StudentiList", studenti);
        }

        [Route("studenti/details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var studente = await _studentiService.GetStudenteByIdAsync(id);

            if (studente == null)
            {
                TempData["Error"] = "Error while finding entity on database";
                return RedirectToAction("Index");
            }

            var dettagliViewModel = new DettagliViewModel()
            {
                Id = studente.Id,
                Name = studente.Name,
                Cognome = studente.Cognome,
                DataNascita = studente.DataNascita,
                Email = studente.Email,
            };

            return Json(dettagliViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
