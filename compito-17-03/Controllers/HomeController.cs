using System.Diagnostics;
using compito_17_03.Models;
using Microsoft.AspNetCore.Mvc;
using compito_17_03.Services;
using compito_17_03.ViewModels;
using AspNetCoreGeneratedDocument;

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

        [Route("studenti/edit/{id:guid}")]
        public async Task<IActionResult> EditStudente(Guid id)
        {
            var studente = await _studentiService.GetStudenteByIdAsync(id);
            var editViewModel = new EditViewModel()
            {
                Id = studente.Id,
                Name = studente.Name,
                Cognome= studente.Cognome,
                DataNascita = studente.DataNascita,
                Email = studente.Email,
            };

            return PartialView("_EditForm", editViewModel);
        }

        [HttpPost("studenti/edit/save")]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            var result = await _studentiService.EditStudenteAsync(editViewModel);

            if (!result)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(new
            {
                success = true
            }); ;
        }

        [HttpPost]
        [Route("studente/delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _studentiService.DeleteStudenteByIdAsync(id);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                });
            }

            return Json(new
            {
                success = true,
            });
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
