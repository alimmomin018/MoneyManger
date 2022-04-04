using SQLite;
using System;

namespace MoneyManger.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Note Notes { get; set; }
        public string Description { get; set; }
    }
}