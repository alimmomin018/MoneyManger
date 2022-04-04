using SQLite;

namespace MoneyManger.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }
        public string Notes { get; set; }
    }
}