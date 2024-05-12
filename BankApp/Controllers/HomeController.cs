using BankApp.Commands;
using BankApp.Models;
using BankApp.Queries;
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

        [HttpPost("api/upload-xml")]
        public async Task<IActionResult> XmlToDb(IFormFile formFile, string bank, string currency)
        {
            await _mediator.Send(new CreateTransactionCommand(formFile, bank, currency));

            return RedirectToAction("GetAll");
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
