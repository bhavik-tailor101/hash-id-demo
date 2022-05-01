using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hashdemo.Dto;
using hashdemo.Model;
using hashdemo.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hashdemo.Controllers
{
    [Route("api/v2/person")]
    public class HashedController : Controller
    {
        private PersonService _personService;
        private IHashids _hashids;

        public HashedController(PersonService personService, IHashids hashids)
        {
            _personService = personService;
            _hashids = hashids;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<PersonDto> Get(string id)
        {
            int decodedId = _hashids.DecodeSingle(id);
            Person person = await _personService.GetPersonByIdAsync(decodedId);
            PersonDto personDto = new PersonDto
            {
                Id = _hashids.Encode(person.Id),
                Name = person.Name,
                Email = person.Email
            };
            return personDto;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<PersonDto> Post([FromBody]PersonPostDto person)
        {
            Person newPerson = await _personService.CreatePersonAsync(person);
            PersonDto personDto = new PersonDto
            {
                Id = _hashids.Encode(newPerson.Id),
                Name = newPerson.Name,
                Email = newPerson.Email
            };

            return personDto;
        }
    }
}
