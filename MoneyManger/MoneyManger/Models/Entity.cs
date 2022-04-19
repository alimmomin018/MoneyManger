using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Linq;
using System.Collections.Generic;

namespace MoneyManger.Models
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int EntityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }
        [Ignore]
        public bool IsSelected { get; set; }
        [Ignore]
        public string LastUpdated
        {
            get
            {
                var lastTransaction = Transactions?.LastOrDefault();
                return lastTransaction != null? lastTransaction.Date.ToString("MMMM dd, yyyy"): "N/A";
            }
        }
        [Ignore]
        public decimal Total
        {
            get
            {
                decimal total = 0;
                if(Transactions != null)
                {
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Type == TransactionType.Income)
                        {
                            total += transaction.Amount;
                        }
                        else if (transaction.Type == TransactionType.Expense)
                        {
                            total -= transaction.Amount;
                        }
                    }
                }
                
                return total;
            }
        }
        [Ignore]
        public decimal TotalIncome
        {
            get
            {
                decimal total = 0;
                if(Transactions != null)
                {
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Type == TransactionType.Income)
                        {
                            total += transaction.Amount;
                        }
                    }
                }
                
                return total;
            }
        }
        [Ignore]
        public decimal TotalExpense
        {
            get
            {
                decimal total = 0;
                if(Transactions != null)
                {
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Type == TransactionType.Expense)
                        {
                            total += transaction.Amount;
                        }
                    }
                }
                
                return total;
            }
        }
    }
}