using BankApp.Commands;
using BankApp.Models;
using BankApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            FormViewModel vm = new FormViewModel();

            vm.RaiffeisenRsd = await _mediator.Send(new GetAllRaiffeisenRsdQuery());

            return View(vm);
        }

        [HttpGet("api/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allData = await _mediator.Send(new GetAllRaiffeisenRsdQuery());

            return Json(allData);
        }

        public async Task<IActionResult> XmlToDbAction(FormViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                vm.RaiffeisenRsd = await _mediator.Send(new GetAllRaiffeisenRsdQuery());

                return View("Index", vm);
            }

            await _mediator.Send(new CreateTransactionCommand(vm.FormFile, vm.Bank, vm.Currency));

            return RedirectToAction("Index");
        }

        [HttpPost("api/upload-xml")]
        public async Task<IActionResult> XmlToDb(IFormFile formFile, string bank, string currency)
        {
            await _mediator.Send(new CreateTransactionCommand(formFile, bank, currency));

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public JsonResult DeleteSelected([FromBody]List<int> ids) 
        {
            _mediator.Send(new DeleteTransactionsCommand(ids));

            return Json(new { success = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorSource, string errorMessage)
        {
            var errorViewModel = new ErrorViewModel
            {
                ErrorSource = errorSource,
                ErrorMessage = errorMessage
            };

            return View(errorViewModel);
        }
    }
}
