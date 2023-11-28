using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models.ContactInfos;
using PhoneBook.Web.Services.ContactInfo;
using PhoneBook.Web.Services.Person;

namespace PhoneBook.Web.Controllers
{
    public class ContactInfosController : Controller
    {
        private readonly IContactInfoService _contactInfoService;

        private readonly IPersonService _personService;
        public ContactInfosController(IContactInfoService contactInfoService, IPersonService personService)
        {
            _contactInfoService = contactInfoService;
            _personService = personService;
        }

        public async Task<IActionResult> ContactInfoList(string personId)
        {
            return PartialView("_ContactInfoList", await _contactInfoService.GetAllContactInfoPersonIdAsync(personId));
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var person = await _personService.GetByPersonId(id);
            ContactInfoCreateInput model = new ContactInfoCreateInput()
            {
                PersonId = id,
                FullName = person.FirstName + " " + person.LastName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInfoCreateInput contactInfoCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View(contactInfoCreateInput);
            }
            await _contactInfoService.CreateContactInfoAsync(contactInfoCreateInput);
            return RedirectToAction("Update", "Persons", new { id = contactInfoCreateInput.PersonId });
        }

        public async Task<IActionResult> Update(string id)
        {
            var contactInfo = await _contactInfoService.GetByContactInfoId(id);
            if (contactInfo == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var person = await _personService.GetByPersonId(contactInfo.PersonId);
            ContactInfoUpdateInput contactInfoUpdateInput = new ContactInfoUpdateInput()
            {
                InfoContent = contactInfo.InfoContent,
                PersonId = contactInfo.PersonId,
                InfoType = contactInfo.InfoType,
                UUID = contactInfo.UUID,
                FullName = person.FirstName + " " + person.LastName
            };

            return View(contactInfoUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactInfoUpdateInput contactInfoUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return View(contactInfoUpdateInput);
            }
            var contactInfo = _contactInfoService.GetByContactInfoId(contactInfoUpdateInput.UUID);
            if (!contactInfo.IsCompleted)
                RedirectToAction(nameof(Index));
            await _contactInfoService.UpdateContactInfoAsync(contactInfoUpdateInput);
            return RedirectToAction("Update","Persons",new { id= contactInfo.Result.PersonId});
        }

        public async Task<IActionResult> Delete(string id)
        {
            var contactInfo = _contactInfoService.GetByContactInfoId(id);
            await _contactInfoService.DeleteContactInfoAsync(id);
            return RedirectToAction("Update", "Persons", new { id = contactInfo.Result.PersonId });
        }
    }
}
