using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hashdemo.Dto;
using hashdemo.Model;
using hashdemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace hashdemo.Controllers
{
    [Route("api/v1/person")]
    public class NonHashedController : Controller
    {
        private PersonService _personService;

        public NonHashedController(PersonService personService)
        {
            _personService = personService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            Person person = await _personService.GetPersonByIdAsync(id);
            return person;
        }

        // POST api/values
        [HttpPost]
        public async Task<Person> Post([FromBody]PersonPostDto person)
        {
            Person newPerson = await _personService.CreatePersonAsync(person);
            return newPerson;
        }
    }
}
