using MoneyManger.Models;
using MoneyManger.Services.Interfaces;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class TransactionDataStore : BaseDataStore, ITransactionDataStore
    {
        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ApplicationException(Constants.TRANSACTION_ADD_FAILED);

            await DbContext.InsertAsync(transaction);
            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            if (transactionId <= 0)
                throw new ApplicationException(Constants.TRANSACTION_NON_ZERO_VALIDATION_FAILED);

            await DbContext.Table<Transaction>().DeleteAsync(x => x.TransactionId == transactionId);
            return true;
        }

        public async Task<Entity> GetAllTransactionsForEntityAsync(int entityId, DateTime? startDate, DateTime endDate)
        {
            if (entityId <= 0)
                throw new ApplicationException(Constants.ENTITY_NON_ZERO_VALIDATION_FAILED);

            if(startDate == null)
                startDate = DateTime.MinValue;

            var entity = await DbContext.GetAsync<Entity>(entityId);
            var transactions = await DbContext.GetAllWithChildrenAsync<Transaction>(
                x => x.EntityId == entityId && x.Date >= startDate && x.Date <= endDate);
            entity.Transactions = transactions;
            
            return entity;
        }

        public async Task<Transaction> GetTransactionAsync(int transactionId)
        {
            if (transactionId <= 0)
                throw new ApplicationException(Constants.TRANSACTION_NON_ZERO_VALIDATION_FAILED);

            var transaction = await DbContext.GetWithChildrenAsync<Transaction>(transactionId);
            return transaction;
        }
        
        public async Task<List<string>> GetAllTransactionNotesAsync()
        {
            var transactions = await DbContext.Table<Transaction>().ToListAsync();
            return transactions.Select(x => x.Notes).Distinct().ToList();
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction?.TransactionId <= 0)
                throw new ApplicationException(Constants.TRANSACTION_NON_ZERO_VALIDATION_FAILED);

            await DbContext.UpdateAsync(transaction);
            return true;
        }
    }
}
