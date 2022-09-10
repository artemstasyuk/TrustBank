using BankApplication.Infrastructure.TransferService;
using Microsoft.AspNetCore.Authorization;

namespace BankApplication.Controllers;

public class TransferController : Controller
{
    private readonly ITransferService _transferService;
    private readonly IOperationRepository _operationRepository;

    public TransferController(ITransferService transferService, ICardRepository cardRepository, IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
        _transferService = transferService;
    }
    
    
    [HttpGet]
    [Authorize]
    public ActionResult Transfer() => View();
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Transfer(int id , TransferOperationViewModel viewModel)
    {
        if (ModelState.IsValid)
        { 
            var operationDto = await _transferService.TransferByCardNumber(id, viewModel.RecipientСardNumber, viewModel.Amount, CardOperationType.Transfer);
            if (operationDto.Status != true)
            {
                ModelState.AddModelError("", $"{operationDto.Error}");
                return View();
            }
            return RedirectToAction("Receipt", operationDto.Operation);
        }

        return View();
    }

    [HttpGet]
    [Authorize]
    public ActionResult Replenish() => View();

    public async Task<ActionResult> Replenish(int id, ReplenishOperationViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var operationDto = await _transferService.ReplenishByCardNumber(id, viewModel.CardNumber, viewModel.Amount,
                viewModel.CVV, viewModel.Validity, CardOperationType.Replenish);
            if (operationDto.Status != true)
            {
                ModelState.AddModelError("", $"{operationDto.Error}");
                return View();
            }
            return RedirectToAction("Receipt", operationDto.Operation);
        }

        return View();
    }
    
    public ActionResult Receipt(Operation operation) => 
        View(operation);
    

    [Authorize]
    public async Task<ActionResult> History(int id) => 
        View(new HistoryViewModel(){CardId = id, Operations = await _operationRepository.GetAllOperations(id)});
    
}