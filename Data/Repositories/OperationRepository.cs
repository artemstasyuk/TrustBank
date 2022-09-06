namespace BankApplication.Data.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly AppDbContext _dbContext;

    public OperationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOperation(Operation operation)
    {
        _dbContext.Operations.Add(operation);
        await SaveAsync();
    }

    public async Task<Operation> GetOperationById(int id) =>
        await _dbContext.Operations.FindAsync(new object[] {id});

    public async Task<List<Operation>> GetAllOperations(int cardId) => 
        await _dbContext.Operations.Where(op => op.CardId.Equals(cardId)).ToListAsync();
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();
}