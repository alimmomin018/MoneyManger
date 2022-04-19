using MoneyManger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services.Interfaces
{
    public interface IEntityDataStore
    {
        Task<bool> AddEntityAsync(Entity item);
        Task<bool> UpdateEntityAsync(Entity item);
        Task<bool> DeleteEntityAsync(int id);
        Task<Entity> GetEntityAsync(int id);
        Task<IEnumerable<Entity>> GetAllEntityAsync();
    }
}
