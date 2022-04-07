using MoneyManger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services.Interfaces
{
    public interface IPersonDataStore
    {
        Task<bool> AddPersonAsync(Person item);
        Task<bool> UpdatePersonAsync(Person item);
        Task<bool> DeletePersonAsync(int id);
        Task<Person> GetPersonAsync(int id);
        Task<IEnumerable<Person>> GetAllPeopleAsync();
    }
}
