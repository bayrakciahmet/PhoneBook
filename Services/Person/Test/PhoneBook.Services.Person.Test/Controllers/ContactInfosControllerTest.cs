using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Services.Person.Controllers;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Services.ContactInfos;
using PhoneBook.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Person.Test.Controllers
{
    public class ContactInfosControllerTest
    {
        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithContactInfoList()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var contactInfos = new List<ContactInfoDto>
        {
            new ContactInfoDto { UUID = "1",InfoType = "Email", InfoContent = "john.doe@example.com" },
            new ContactInfoDto { UUID = "2", InfoType = "Email", InfoContent = "jane.doe@example.com" }
        };

            var response = Response<List<ContactInfoDto>>.Success(contactInfos, 200);
            contactInfoServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(response);

            // Act
            var result = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<List<ContactInfoDto>>>(objectResult.Value);

            Assert.True(objectResult.StatusCode.HasValue);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }
        // GetById
        [Fact]
        public async Task GetById_ShouldReturnOkResultWithContactInfo()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var contactInfoId = "1";
            var contactInfoDto = new ContactInfoDto { UUID = "2", InfoType = "Email", InfoContent = "jane.doe@example.com" };

            var response = Response<ContactInfoDto>.Success(contactInfoDto, 200);
            contactInfoServiceMock.Setup(m => m.GetByIdAsync(contactInfoId)).ReturnsAsync(response);

            // Act
            var result = await controller.GetById(contactInfoId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<ContactInfoDto>>(objectResult.Value);

            Assert.True(objectResult.StatusCode.HasValue);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // GetAllByPersonId
        [Fact]
        public async Task GetAllByPersonId_ShouldReturnOkResultWithContactInfoList()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var personId = "1";
            var contactInfos = new List<ContactInfoDto>
    {
        new ContactInfoDto {  UUID = "1", InfoType = "Email", InfoContent = "jane.doe@example.com" },
        new ContactInfoDto {  UUID = "2", InfoType = "Email", InfoContent = "jane.doe@example.com" }
    };

            var response = Response<List<ContactInfoDto>>.Success(contactInfos, 200);
            contactInfoServiceMock.Setup(m => m.GetAllByPersonIdAsync(personId)).ReturnsAsync(response);

            // Act
            var result = await controller.GetAllByPersonId(personId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<List<ContactInfoDto>>>(objectResult.Value);

            Assert.True(objectResult.StatusCode.HasValue);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // Create
        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResultWithCreatedContactInfo()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var contactInfoCreateDto = new ContactInfoCreateDto { InfoType = "Email", InfoContent = "jane.doe@example.com" };
            var newContactInfoDto = new ContactInfoDto { UUID = "2", InfoType = "Email", InfoContent = "jane.doe@example.com" };

            var response = Response<ContactInfoDto>.Success(newContactInfoDto, 201);
            contactInfoServiceMock.Setup(m => m.CreateAsync(contactInfoCreateDto)).ReturnsAsync(response);

            // Act
            var result = await controller.Create(contactInfoCreateDto);

            // Assert
            var createdAtActionResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<ContactInfoDto>>(createdAtActionResult.Value);

            Assert.Equal(response.StatusCode, createdAtActionResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // Update
        [Fact]
        public async Task Update_ShouldReturnNoContentResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var contactInfoUpdateDto = new ContactInfoUpdateDto { UUID = "2", InfoType = "Email", InfoContent = "jane.doe@example.com" };

            var response = Response<NoContent>.Success(204);
            contactInfoServiceMock.Setup(m => m.UpdateAsync(contactInfoUpdateDto)).ReturnsAsync(response);

            // Act
            var result = await controller.Update(contactInfoUpdateDto);

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(response.StatusCode, noContentResult.StatusCode);
        }

        // Delete
        [Fact]
        public async Task Delete_ShouldReturnNoContentResult()
        {
            // Arrange
            var contactInfoServiceMock = new Mock<IContactInfoService>();
            var controller = new ContactInfosController(contactInfoServiceMock.Object);

            var contactInfoId = "1";

            var response = Response<NoContent>.Success(204);
            contactInfoServiceMock.Setup(m => m.DeleteAsync(contactInfoId)).ReturnsAsync(response);

            // Act
            var result = await controller.Delete(contactInfoId);

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(response.StatusCode, noContentResult.StatusCode);

        }
    }
}