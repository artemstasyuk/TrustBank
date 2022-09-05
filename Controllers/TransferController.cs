using BankApplication.Extensions;
using BankApplication.Infrastructure.TransferService;
using Twilio.Rest.Api.V2010;

namespace BankApplication.Controllers;

public class TransferController : Controller
{
    private readonly ITransferService _transferService;
    private readonly ICardRepository _cardRepository;

    public TransferController(ITransferService transferService, ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
        _transferService = transferService;
    }

    [HttpGet]
    public async Task<ActionResult> Index(int id)
    {
        var card = await _cardRepository.GetCardByIdAsync(id);
        return View(card);
    }

    [HttpGet]
    public ActionResult Transfer() => View();
    
    [HttpPost]
    public async Task<ActionResult> Transfer(int id ,OperationViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var operation = _transferService.TransferByCardNumber(id, viewModel.RecipientСardNumber, viewModel.Amount);
            return RedirectToAction("Receipt", operation);
        }

        return View();
    }

    public ActionResult Receipt(Operation operation)
    {
        return View(operation);
    }
}