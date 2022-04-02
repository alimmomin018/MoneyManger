using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManger.Services
{
    public class MockDataStore : IDataStore<Person>
    {
        readonly List<Person> peoples;

        public MockDataStore()
        {
            peoples = new List<Person>()
            {

            };
        }

        public async Task<bool> AddPersonAsync(Person item)
        {
            peoples.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePersonAsync(Person item)
        {
            var oldItem = peoples.Where((Person arg) => arg.Id == item.Id).FirstOrDefault();
            peoples.Remove(oldItem);
            peoples.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeletePersonAsync(string id)
        {
            var oldItem = peoples.Where((Person arg) => arg.Id == id).FirstOrDefault();
            peoples.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            return await Task.FromResult(peoples.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Person>> GetPeoplesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(peoples);
        }
    }
}