using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Services.ContactInfos;
using PhoneBook.Shared.ControllerBases;

namespace PhoneBook.Services.Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfosController : CustomBaseController
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfosController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactInfoService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _contactInfoService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllByPersonId/{personId}")]
        public async Task<IActionResult> GetAllByPersonId(string personId)
        {
            var response = await _contactInfoService.GetAllByPersonIdAsync(personId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInfoCreateDto contactInfoCreateDto)
        {
            var response = await _contactInfoService.CreateAsync(contactInfoCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactInfoUpdateDto contactInfoUpdateDto)
        {
            var response = await _contactInfoService.UpdateAsync(contactInfoUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _contactInfoService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
