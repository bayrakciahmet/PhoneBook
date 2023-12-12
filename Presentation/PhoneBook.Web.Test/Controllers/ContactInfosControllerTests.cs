using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Controllers;
using PhoneBook.Web.Helpers;
using PhoneBook.Web.Models;
using PhoneBook.Web.Models.ContactInfos;
using PhoneBook.Web.Models.Persons;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.Test.Controllers
{
    public class ContactInfosControllerTests
    {      
        [Fact]
        public async Task ContactInfoList_ReturnsPartialViewWithModel()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            contactInfoServiceMock.Setup(x => x.GetAllContactInfoPersonIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<ContactInfoViewModel>());

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            // Act
            var result = await controller.ContactInfoList("1") as PartialViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("_ContactInfoList", result.ViewName);
            Assert.NotNull(result.Model);
            Assert.IsType<List<ContactInfoViewModel>>(result.Model);
        }
        [Fact]
        public async Task Create_Get_ReturnsPartialViewWithModel()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            personServiceMock.Setup(x => x.GetByPersonId(It.IsAny<string>()))
                .ReturnsAsync(new PersonViewModel());

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            // Act
            var result = await controller.Create("1") as PartialViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("_Create", result.ViewName);
            Assert.NotNull(result.Model);
            Assert.IsType<ContactInfoCreateInput>(result.Model);
        }

        [Fact]
        public async Task Create_Post_InvalidModelState_ReturnsJsonResultWithError()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);
            controller.ModelState.AddModelError("InfoContent", "InfoContent is required");

            // Act
            var result = await controller.Create(new ContactInfoCreateInput()) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var responseData = result.Value as GenerateModelError;
           
            Assert.NotNull(responseData);
            Assert.Equal("Başarısız", responseData.Title);
            Assert.Contains("InfoContent is required", responseData.Error);
        }

        [Fact]
        public async Task Create_Post_ValidModelState_ReturnsJsonResultWithSuccess()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfoCreateInput>()))
                .ReturnsAsync(new Response<ContactInfoViewModel> { IsSuccessful = true });

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            // Act
            var result = await controller.Create(new ContactInfoCreateInput()) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var responseData = result.Value as MyCustomResponse;
            Assert.NotNull(responseData);
            Assert.Equal("Başarılı", responseData.Title);
            Assert.Equal("Kaydedildi", responseData.Info);
        }

        [Fact]
        public async Task Update_Post_InvalidModelState_ReturnsJsonResultWithError()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);
            controller.ModelState.AddModelError("InfoContent", "InfoContent is required");

            // Act
            var result = await controller.Update(new ContactInfoUpdateInput()) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var responseData = result.Value as GenerateModelError;
            Assert.NotNull(responseData);
            Assert.Equal("Başarısız", responseData.Title);
            Assert.Contains("InfoContent is required", responseData.Error);
        }

        [Fact]
        public async Task Update_Post_ValidModelState_ReturnsJsonResultWithSuccess()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            contactInfoServiceMock.Setup(x => x.GetByContactInfoId(It.IsAny<string>()))
                .ReturnsAsync(new ContactInfoViewModel());
            contactInfoServiceMock.Setup(x => x.UpdateContactInfoAsync(It.IsAny<ContactInfoUpdateInput>()))
                .ReturnsAsync(new Response<NoContent> { IsSuccessful = true });

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            // Act
            var result = await controller.Update(new ContactInfoUpdateInput()) as JsonResult;

            // Assert
            Assert.NotNull(result);
            var responseData = result.Value as MyCustomResponse;
            Assert.NotNull(responseData);
            Assert.Equal("Başarılı", responseData.Title);
            Assert.Equal("Kaydedildi", responseData.Info);
        }

        [Fact]
        public async Task Delete_ValidContactInfoId_ReturnsJsonResultWithSuccess()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var personServiceMock = new Mock<IPersonService>();
            contactInfoServiceMock.Setup(x => x.GetByContactInfoId(It.IsAny<string>()))
                .ReturnsAsync(new ContactInfoViewModel());
            contactInfoServiceMock.Setup(x => x.DeleteContactInfoAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var controller = new ContactInfosController(contactInfoServiceMock.Object, personServiceMock.Object);

            // Act
            var result = await controller.Delete("1") as JsonResult;

            // Assert
            Assert.NotNull(result);
            var responseData = result.Value as MyCustomResponse;
            Assert.NotNull(responseData);
            Assert.Equal("Başarılı", responseData.Title);
            Assert.Equal("Silindi", responseData.Info);
        }
    }
}
