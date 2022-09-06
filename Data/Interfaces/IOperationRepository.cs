namespace BankApplication.Data.Interfaces;

public interface IOperationRepository
{
    Task CreateOperation(Operation operation);
    Task<Operation> GetOperationById(int id);
    Task<List<Operation>> GetAllOperations(int cardId);
}