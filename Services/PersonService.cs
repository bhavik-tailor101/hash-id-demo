using hashdemo.Dto;
using hashdemo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace hashdemo.Services
{
    public class PersonService
    {
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            IList<Person> person = await GetAllPersons();
            return person.First(x => x.Id == id);
        }

        public async Task<Person> CreatePersonAsync(PersonPostDto person)
        {
            IList<Person> persons = await GetAllPersons();
            int count = persons.Count;
            Person newPerson = new Person
            {
                Id = count + 1,
                Name = person.Name,
                Email = person.Email
            };
            persons.Add(newPerson);
            await WriteListToJsonAsync(persons);
            return newPerson;
        }

        public async Task<IList<Person>> GetAllPersons()
        {
            string json = await File.ReadAllTextAsync(@"./DataSource/data.json");
            IList<Person> retList = JsonSerializer.Deserialize<IList<Person>>(json);

            return retList;
        }

        public async Task WriteListToJsonAsync(IList<Person> persons)
        {
            string json = JsonSerializer.Serialize(persons);
            await File.WriteAllTextAsync(@"./DataSource/data.json", json);
        }
    }
}
