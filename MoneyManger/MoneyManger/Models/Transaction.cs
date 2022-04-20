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
        public string Notes { get; set; }
        public string Description { get; set; }
        [ForeignKey(typeof(Entity))]
        public int EntityId { get; set; }
        [Ignore]
        public bool IsIncome => Type == TransactionType.Income; 
    }
}