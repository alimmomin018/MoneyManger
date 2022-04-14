using MoneyManger.Models;
using SQLite;
using System.IO;
using Xamarin.Essentials;

namespace MoneyManger.Services
{
    public class BaseDataStore
    {
        public SQLiteAsyncConnection DbContext;

        public BaseDataStore()
        {
            IntializeDb();
        }

        private async void IntializeDb()
        {
            if (DbContext != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, Constants.DB_NAME);
            DbContext = new SQLiteAsyncConnection(databasePath);

            await DbContext.CreateTableAsync<Person>();
            await DbContext.CreateTableAsync<Transaction>();
            await DbContext.CreateTableAsync<Note>();
        }
    }
}
