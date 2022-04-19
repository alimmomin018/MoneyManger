using MoneyManger.Models;
using MoneyManger.Services.Interfaces;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class EntityDataStore : BaseDataStore, IEntityDataStore
    {
        public async Task<bool> AddEntityAsync(Entity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new ApplicationException(Constants.ENTITY_ADD_FAILED);

            await DbContext.InsertAsync(entity);
            return true;
        }

        public async Task<bool> DeleteEntityAsync(int entityId)
        {
            if (entityId <= 0)
                throw new ApplicationException(Constants.ENTITY_NON_ZERO_VALIDATION_FAILED);

            await DbContext.Table<Entity>().DeleteAsync(x => x.EntityId == entityId);
            return true;
        }

        public async Task<IEnumerable<Entity>> GetAllEntityAsync()
        {
            var peoples = await DbContext.GetAllWithChildrenAsync<Entity>(x=> x.IsActive);
            return peoples;
        }

        public async Task<Entity> GetEntityAsync(int entityId)
        {
            if (entityId <= 0)
                throw new ApplicationException(Constants.ENTITY_NON_ZERO_VALIDATION_FAILED);

            var entity = await DbContext.Table<Entity>().FirstOrDefaultAsync(x => x.EntityId == entityId);
            return entity;
        }

        public async Task<bool> UpdateEntityAsync(Entity entity)
        {
            if (entity?.EntityId == null)
                throw new ApplicationException(Constants.ENTITY_NON_ZERO_VALIDATION_FAILED);

            await DbContext.UpdateAsync(entity);
            return true;
        }
    }
}
