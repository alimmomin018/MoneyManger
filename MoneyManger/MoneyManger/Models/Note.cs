using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MoneyManger.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }
        public string Notes { get; set; }
        [ForeignKey(typeof(Transaction))]
        public int TransactionId { get; set; }
    }
}