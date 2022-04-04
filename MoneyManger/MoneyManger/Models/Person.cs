using SQLite;
using System;
using System.Collections.Generic;

namespace MoneyManger.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Transaction> Transaction { get; set; }
        [Ignore]
        public string LastUpdated { get; set; }
        [Ignore]
        public decimal Total { get; set; }
    }
}