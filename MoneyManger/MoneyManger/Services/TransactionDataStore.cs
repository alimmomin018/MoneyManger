using MoneyManger.Models;
using MoneyManger.Services.Interfaces;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class TransactionDataStore : BaseDataStore, ITransactionDataStore
    {
        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            if (transaction == null)
                throw new ApplicationException(Constants.TRANSACTION_ADD_FAILED);

            await DbContext.InsertWithChildrenAsync(transaction);
            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            if (transactionId <= 0)
                throw new ApplicationException(Constants.TRANSACTION_NON_ZERO_VALIDATION_FAILED);

            await DbContext.Table<Transaction>().DeleteAsync(x => x.TransactionId == transactionId);
            return true;
        }

        public async Task<Entity> GetAllTransactionsForEntityAsync(int entityId)
        {
            if (entityId <= 0)
                throw new ApplicationException(Constants.ENTITY_NON_ZERO_VALIDATION_FAILED);

            var entity = await DbContext.GetAsync<Entity>(entityId);
            var transactions = await DbContext.GetAllWithChildrenAsync<Transaction>(x => x.EntityId == entityId);
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

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction?.TransactionId <= 0)
                throw new ApplicationException(Constants.TRANSACTION_NON_ZERO_VALIDATION_FAILED);

            await DbContext.UpdateAsync(transaction);
            await DbContext.UpdateAsync(transaction.Notes);
            return true;
        }
    }
}
