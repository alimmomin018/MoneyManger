using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Linq;
using System.Collections.Generic;

namespace MoneyManger.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }
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
                            total += total;
                        }
                        else if (transaction.Type == TransactionType.Expense)
                        {
                            total -= total;
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
                            total += total;
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
                            total += total;
                        }
                    }
                }
                
                return total;
            }
        }
    }
}