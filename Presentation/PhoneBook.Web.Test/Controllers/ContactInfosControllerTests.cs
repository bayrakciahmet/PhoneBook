using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Web.Controllers;
using PhoneBook.Web.Models.ContactInfos;
using PhoneBook.Web.Models.Persons;
using PhoneBook.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Web.Test.Controllers
{
    public class ContactInfosControllerTests
    {
        [Fact]
        public async Task ContactInfoList_Returns_PartialViewResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            var personId = "samplePersonId";
            var contactInfos = new List<ContactInfoViewModel>(); 

            contactInfoServiceMock.Setup(x => x.GetAllContactInfoPersonIdAsync(personId))
                .ReturnsAsync(contactInfos);

            // Act
            var result = await controller.ContactInfoList(personId);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_ContactInfoList", partialViewResult.ViewName);
        }

        [Fact]
        public async Task Create_Get_Returns_ViewResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            var personId = "personId";
            var person = new PersonViewModel(); 

            personServiceMock.Setup(x => x.GetByPersonId(personId))
                .ReturnsAsync(person);

            // Act
            var result = await controller.Create(personId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ContactInfoCreateInput>(viewResult.Model);
        }

        [Fact]
        public async Task Create_Post_Returns_RedirectToAction()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            var contactInfoCreateInput = new ContactInfoCreateInput(); 

            contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(contactInfoCreateInput))
                .ReturnsAsync(true);

            // Act
            var result = await controller.Create(contactInfoCreateInput);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Update", redirectToActionResult.ActionName);
            Assert.Equal("Persons", redirectToActionResult.ControllerName);
            Assert.Equal(contactInfoCreateInput.PersonId, redirectToActionResult.RouteValues["id"]);
        }


        [Fact]
        public async Task Delete_Returns_RedirectToAction()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            var contactInfoId = "sampleContactInfoId";

            contactInfoServiceMock.Setup(x => x.GetByContactInfoId(contactInfoId))
                .ReturnsAsync(new ContactInfoViewModel() { PersonId = "samplePersonId" });

            // Act
            var result = await controller.Delete(contactInfoId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Update", redirectToActionResult.ActionName);
            Assert.Equal("Persons", redirectToActionResult.ControllerName);
            Assert.Equal("samplePersonId", redirectToActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Create_Post_ValidationFailure_Returns_ViewResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);
            controller.ModelState.AddModelError("InfoType", "Bilgi Tipi Alanı Gerekli");

            var contactInfoCreateInput = new ContactInfoCreateInput();

            // Act
            var result = await controller.Create(contactInfoCreateInput);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(contactInfoCreateInput, viewResult.Model);
        }

        [Fact]
        public async Task Update_Post_ValidationFailure_Returns_ViewResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);
            controller.ModelState.AddModelError("InfoType", "Bilgi Tipi Alanı Gerekli");

            var contactInfoUpdateInput = new ContactInfoUpdateInput();

            // Act
            var result = await controller.Update(contactInfoUpdateInput);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(contactInfoUpdateInput, viewResult.Model);
        }
    }
}
