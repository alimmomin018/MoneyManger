using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace MoneyManger.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public Note Notes { get; set; }
        public string Description { get; set; }
    }
}