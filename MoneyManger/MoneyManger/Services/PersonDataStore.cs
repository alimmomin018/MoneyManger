using MoneyManger.Models;
using MoneyManger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class PersonDataStore : BaseDataStore, IPersonDataStore
    {
        public PersonDataStore()
        {

        }

        public async Task<bool> AddPersonAsync(Person person)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(person.Name))
                    throw new ApplicationException("Person Name cannot be null or empty");

                await DbContext.InsertAsync(person);
                return true;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            try
            {
                if (personId <= 0)
                    throw new ApplicationException("Person Id cannot be less then zero");

                await DbContext.Table<Person>().DeleteAsync(x => x.Id == personId);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Person>> GetAllPeopleAsync()
        {
            try
            {
                var peoples = await DbContext.Table<Person>().Where(x => !x.IsActive).ToListAsync();
                return peoples;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<Person> GetPersonAsync(int personId)
        {
            try
            {
                if(personId <= 0)
                    throw new ApplicationException("Person Id cannot be less then zero");

                var person = await DbContext.Table<Person>().FirstOrDefaultAsync(x => x.Id == personId);
                return person;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            try
            {
                if (person?.Id == null)
                    return false;

                await DbContext.UpdateAsync(person);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
