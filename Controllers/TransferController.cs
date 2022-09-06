using BankApplication.Extensions;
using BankApplication.Infrastructure.TransferService;
using Twilio.Rest.Api.V2010;

namespace BankApplication.Controllers;

public class TransferController : Controller
{
    private readonly ITransferService _transferService;
    private readonly ICardRepository _cardRepository;
    private readonly IOperationRepository _operationRepository;

    public TransferController(ITransferService transferService, ICardRepository cardRepository, IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
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
            var opId = await _transferService.TransferByCardNumber(id, viewModel.RecipientСardNumber, viewModel.Amount, CardOperationType.Transfer);
            return RedirectToAction("Receipt", opId);
        }

        return View();
    }

    [HttpGet]
    public ActionResult Replenish() => View();

    public async Task<ActionResult> Replenish(int id, OperationViewModel viewModel)
    {
        if (ModelState.IsValid)
        {            
            var operation = await _transferService.TransferByCardNumber(id, viewModel.RecipientСardNumber, viewModel.Amount, CardOperationType.Replenish);
            return RedirectToAction("Receipt", operation);
        }

        return View();
    }


    public ActionResult Receipt(Operation operation)
    {
        return View(operation);
    }

    public async Task<ActionResult> History(int id)
    {
        List<Operation> operations = await _operationRepository.GetAllOperations(id);
        return View(operations);
    }
}