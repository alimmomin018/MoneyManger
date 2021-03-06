using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services.Interfaces
{
    public interface ITransactionDataStore
    {
        Task<bool> AddTransactionAsync(Transaction transaction);
        Task<bool> UpdateTransactionAsync(Transaction transaction);
        Task<bool> DeleteTransactionAsync(int transactionId);
        Task<Transaction> GetTransactionAsync(int transactionId);
        Task<List<string>> GetAllTransactionNotesAsync();
        Task<Entity> GetAllTransactionsForEntityAsync(int entityId, DateTime? startDate, DateTime endDate);
    }
}
