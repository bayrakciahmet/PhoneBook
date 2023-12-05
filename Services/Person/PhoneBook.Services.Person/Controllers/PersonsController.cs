using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Services.Interfaces;
using PhoneBook.Shared.ControllerBases;

namespace PhoneBook.Services.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : CustomBaseController
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _personService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _personService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            var response = await _personService.CreateAsync(personCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PersonUpdateDto personUpdateDto)
        {
            var response = await _personService.UpdateAsync(personUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _personService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
