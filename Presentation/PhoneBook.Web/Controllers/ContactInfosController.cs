using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Extensions;
using PhoneBook.Web.Helpers;
using PhoneBook.Web.Models.ContactInfos;
using PhoneBook.Web.Services.Interfaces;

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
            ViewData["PersonId"] = personId;
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
            return PartialView("_Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactInfoCreateInput contactInfoCreateInput)
        {
            if (!ModelState.IsValid)
                return ControllerHelper.GenerateModelErrorJsonResult(ModelState);
            var response = await _contactInfoService.CreateContactInfoAsync(contactInfoCreateInput);
            if (response.Errors != null)
                return this.CustomJsonResponse(title: LocalizationHelper.SorryText, error: string.Join(", ", response.Errors));
            else
                return this.CustomJsonResponse(title: LocalizationHelper.SuccessTitle, info: LocalizationHelper.SuccessInfo, popup: true);
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

            return PartialView("_Update", contactInfoUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactInfoUpdateInput contactInfoUpdateInput)
        {
            if (!ModelState.IsValid)
                return ControllerHelper.GenerateModelErrorJsonResult(ModelState);

            var contactInfo = await _contactInfoService.GetByContactInfoId(contactInfoUpdateInput.UUID);
            if (contactInfo == null)
                return this.CustomJsonResponse(title: LocalizationHelper.SorryText, error: LocalizationHelper.NotFoundError);
            var response = await _contactInfoService.UpdateContactInfoAsync(contactInfoUpdateInput);
            if (response.IsSuccessful == true)
                return this.CustomJsonResponse(title: LocalizationHelper.SuccessTitle, info: LocalizationHelper.SuccessInfo, popup: true);
            else
                return this.CustomJsonResponse(title: LocalizationHelper.SorryText, error: string.Join(", ", response.Errors));

        }

        public async Task<IActionResult> Delete(string id)
        {
            var contactInfo = await _contactInfoService.GetByContactInfoId(id);
            if (contactInfo == null)
                return this.CustomJsonResponse(title: LocalizationHelper.SorryText, error: LocalizationHelper.NotFoundError);
            await _contactInfoService.DeleteContactInfoAsync(id);
            return this.CustomJsonResponse(title: LocalizationHelper.SuccessTitle, info: LocalizationHelper.DeleteInfo, popup: true);
        }
    }
}
