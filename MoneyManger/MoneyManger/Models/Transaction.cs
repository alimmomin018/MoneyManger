using System;

namespace MoneyManger.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }
}