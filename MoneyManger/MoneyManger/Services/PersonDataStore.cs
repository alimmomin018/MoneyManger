using MoneyManger.Models;
using MoneyManger.Services.Interfaces;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class PersonDataStore : BaseDataStore, IPersonDataStore
    {
        public async Task<bool> AddPersonAsync(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.Name))
                throw new ApplicationException(Constants.PERSON_ADD_FAILED);

            await DbContext.InsertAsync(person);
            return true;
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            if (personId <= 0)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            await DbContext.Table<Person>().DeleteAsync(x => x.PersonId == personId);
            return true;
        }

        public async Task<IEnumerable<Person>> GetAllPeopleAsync()
        {
            var peoples = await DbContext.GetAllWithChildrenAsync<Person>(x=> x.IsActive);
            return peoples;
        }

        public async Task<Person> GetPersonAsync(int personId)
        {
            if (personId <= 0)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            var person = await DbContext.Table<Person>().FirstOrDefaultAsync(x => x.PersonId == personId);
            return person;
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            if (person?.PersonId == null)
                throw new ApplicationException(Constants.PERSON_NON_ZERO_VALIDATION_FAILED);

            await DbContext.UpdateAsync(person);
            return true;
        }
    }
}
