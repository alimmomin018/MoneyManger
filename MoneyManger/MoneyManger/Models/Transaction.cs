using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

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
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TransactionPhoto> Photos { get; set; }
        [ForeignKey(typeof(Entity))]
        public int EntityId { get; set; }
        [Ignore]
        public bool IsIncome => Type == TransactionType.Income; 
    }

    public class TransactionPhoto
    {
        [PrimaryKey, AutoIncrement]
        public int PhotoId { get; set; }
        public byte[] Photo { get; set; }
        [ForeignKey(typeof(Transaction))]
        public int TransactionId { get; set; }
    }
}