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

            vm.Transactions = await _mediator.Send(new GetAllTransactionsQuery());

            return View(vm);
        }

        [HttpGet("api/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allData = await _mediator.Send(new GetAllTransactionsQuery());

            return Json(allData);
        }

        public async Task<IActionResult> XmlToDbAction(FormViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                vm.Transactions = await _mediator.Send(new GetAllTransactionsQuery());

                return View("Index", vm);
            }

            await _mediator.Send(new CreateTransactionCommand(vm.FormFiles, vm.Bank, vm.Currency));

            return RedirectToAction("Index");
        }

        [HttpPost("api/upload-xml")]
        public async Task<IActionResult> XmlToDb(IEnumerable<IFormFile> formFiles, string bank, string currency)
        {
            await _mediator.Send(new CreateTransactionCommand(formFiles, bank, currency));

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public JsonResult DeleteSelected([FromBody]List<int> ids) 
        {
            if (!ids.Any()) 
            { 
                return Json(new { success = false, message = "No IDs recieved." });
            }

            _mediator.Send(new DeleteTransactionsCommand(ids));

            return Json(new { success = true });
        }

        [HttpPost("api/delete-transaction")]
        public JsonResult DeleteSelectedTransactions(List<int> ids)
        {
            if (!ids.Any())
            {
                return Json(new { success = false, message = "No IDs recieved." });
            }

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
