﻿using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models.Persons;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.Controllers
{

    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IContactInfoService _contactInfoService;
        public PersonsController(IPersonService personService, IContactInfoService contactInfoService)
        {
            _personService = personService;
            _contactInfoService = contactInfoService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _personService.GetAllPersonAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PersonCreateInput());
        }
        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateInput personCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View(personCreateInput);
            }
            await _personService.CreatePersonAsync(personCreateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var person = await _personService.GetByPersonId(id);
            if (person == null)
            {
                return RedirectToAction(nameof(Index));
            }
            PersonUpdateInput personUpdateInput = new PersonUpdateInput()
            {
                Company = person.Company,
                FirstName = person.FirstName,
                LastName = person.LastName,
                UUID = person.UUID,
                ContactInfos = await _contactInfoService.GetAllContactInfoPersonIdAsync(person.UUID)
            };
            return View(personUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonUpdateInput personUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                personUpdateInput.ContactInfos = await _contactInfoService.GetAllContactInfoPersonIdAsync(personUpdateInput.UUID);
                return View(personUpdateInput);
            }
            var person = _personService.GetByPersonId(personUpdateInput.UUID);
            if (!person.IsCompleted)
                RedirectToAction(nameof(Index));
            await _personService.UpdatePersonAsync(personUpdateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _personService.DeletePersonAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
