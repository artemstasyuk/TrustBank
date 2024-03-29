﻿namespace BankApplication.Data.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly AppDbContext _dbContext;

    public OperationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOperation(Operation operation)
    {
        await _dbContext.Operations.AddAsync(operation);
        await SaveAsync();
    }

    public async Task<Operation> GetOperationById(int id) =>
        await _dbContext.Operations.FindAsync(new object[] {id});

    public async Task<List<Operation>> GetAllOperations(int cardId) => 
        await _dbContext.Operations.Include(card => card.CardFrom).Include(card => card.CardTo).Where(op => op.CardFromId.Equals(cardId) || op.CardToId.Equals(cardId)).ToListAsync();
    public async Task SaveAsync() => 
        await _dbContext.SaveChangesAsync();
}