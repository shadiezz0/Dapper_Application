using Dapper_Application.Data.DataAccess;
using Dapper_Application.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Application.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _db;

        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddAsync(Person person)
        {
            try
            {
                await _db.SaveData("InsertPerson", new { person.Name, person.Email, person.Address });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {

                await _db.SaveData("UpdatePerson", person);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("DeletePerson", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            IEnumerable<Person> result = await _db.GetData<Person, dynamic>("GetPerson", new { id = id });
            return result.FirstOrDefault();
        }


        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            string query = "GetAll";
            return await _db.GetData<Person, dynamic>(query, new { });
        }

    }
}
