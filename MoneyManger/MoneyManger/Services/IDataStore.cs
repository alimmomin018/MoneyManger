using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddPersonAsync(T item);
        Task<bool> UpdatePersonAsync(T item);
        Task<bool> DeletePersonAsync(string id);
        Task<T> GetPersonAsync(string id);
        Task<IEnumerable<T>> GetPeoplesAsync(bool forceRefresh = false);
    }
}
