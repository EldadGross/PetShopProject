using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Data.Model;
using PetShop.Service.Interfaces;
using System.Diagnostics;

namespace PetShop.Client.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAnimalService<Animal> animalService;
        public HomeController(ILogger<HomeController> logger, IAnimalService<Animal> animalService)
        {
            this.logger = logger;
            this.animalService = animalService;
        }

        public async Task<IActionResult> Index()
        {
            var towAnimals = animalService.GetTwoPopulareAnimals();
            return View(await towAnimals);
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