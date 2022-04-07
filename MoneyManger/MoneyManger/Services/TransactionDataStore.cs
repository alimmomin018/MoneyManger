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
        public TransactionDataStore()
        {

        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                await DbContext.InsertAsync(transaction);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            try
            {
                if (transactionId <= 0)
                    throw new ApplicationException("transactionId cannot be less then zero");

                await DbContext.Table<Transaction>().DeleteAsync(x => x.TransactionId == transactionId);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsForPersonAsync(int personId)
        {
            try
            {
                var person = await DbContext.GetWithChildrenAsync<Person>(personId); 
                if(person?.Transactions == null)
                    return Enumerable.Empty<Transaction>();
                else 
                    return person.Transactions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<Transaction> GetTransactionAsync(int transactionId)
        {
            try
            {
                var transaction = await DbContext.Table<Transaction>().FirstOrDefaultAsync(x => x.TransactionId == transactionId);
                return transaction;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            try
            {
                if (transaction?.TransactionId <= 0)
                    return false;

                await DbContext.UpdateAsync(transaction);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
