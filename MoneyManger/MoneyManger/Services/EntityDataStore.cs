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
        public async Task<bool> AddEntityAsync(Entity person)
        {
            if (string.IsNullOrWhiteSpace(person.Name))
                throw new ApplicationException(Constants.PERSON_ADD_FAILED);

            await DbContext.InsertAsync(person);
            return true;
        }

        public async Task<bool> DeleteEntityAsync(int personId)
        {
            if (personId <= 0)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            await DbContext.Table<Entity>().DeleteAsync(x => x.EntityId == personId);
            return true;
        }

        public async Task<IEnumerable<Entity>> GetAllEntityAsync()
        {
            var peoples = await DbContext.GetAllWithChildrenAsync<Entity>(x=> x.IsActive);
            return peoples;
        }

        public async Task<Entity> GetEntityAsync(int personId)
        {
            if (personId <= 0)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            var person = await DbContext.Table<Entity>().FirstOrDefaultAsync(x => x.EntityId == personId);
            return person;
        }

        public async Task<bool> UpdateEntityAsync(Entity person)
        {
            if (person?.EntityId == null)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            await DbContext.UpdateAsync(person);
            return true;
        }
    }
}
