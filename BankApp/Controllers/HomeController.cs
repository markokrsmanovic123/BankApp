using BankApp.Models;
using BankApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IUnitOfWork _unitOfWork { get; private set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var allData = await _unitOfWork.RaiffeisenRsdRepository.GetAllAsync();

            return View(allData);
        }

        [HttpGet("api/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allData = await _unitOfWork.RaiffeisenRsdRepository.GetAllAsync();

            return Json(allData);
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
