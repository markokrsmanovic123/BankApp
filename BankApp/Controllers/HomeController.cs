using BankApp.Models;
using BankApp.Queries;
using BankApp.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var allData = await _mediator.Send(new GetAllRaiffeisenRsdQuery());

            return View(allData);
        }

        [HttpGet("api/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allData = await _mediator.Send(new GetAllRaiffeisenRsdQuery());

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
