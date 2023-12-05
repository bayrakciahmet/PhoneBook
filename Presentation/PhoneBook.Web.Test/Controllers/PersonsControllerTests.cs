using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Web.Controllers;
using PhoneBook.Web.Models.Persons;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.Test.Controllers
{
    public class PersonsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithPersonList()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var contactInfoServiceMock = new Mock<IContactInfoService>();

            var personsController = new PersonsController(personServiceMock.Object, contactInfoServiceMock.Object);

            var personList = new List<PersonViewModel>
        {
            new PersonViewModel { UUID = "1", FirstName = "John", LastName = "Doe", Company = "ABC Inc.", CreatedTime = DateTime.Now, ContactInfoCount = 3 }
        };

            personServiceMock.Setup(service => service.GetAllPersonAsync()).ReturnsAsync(personList);

            // Act
            var result = await personsController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(personList, result.Model);
        }

        [Fact]
        public async Task CreatePost_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var contactInfoServiceMock = new Mock<IContactInfoService>();

            var personsController = new PersonsController(personServiceMock.Object, contactInfoServiceMock.Object);

            var validPersonCreateInput = new PersonCreateInput { FirstName = "John", LastName = "Doe", Company = "ABC Inc." };

            // Act
            var result = await personsController.Create(validPersonCreateInput) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
        [Fact]
        public async Task CreatePost_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var contactInfoServiceMock = new Mock<IContactInfoService>();

            var personsController = new PersonsController(personServiceMock.Object, contactInfoServiceMock.Object);

            var validPersonCreateInput = new PersonCreateInput { FirstName = "John", LastName = "Doe", Company = "ABC Inc." };

            // Act
            var result = await personsController.Create(validPersonCreateInput) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


        [Fact]
        public async Task UpdatePost_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var contactInfoServiceMock = new Mock<IContactInfoService>();

            var personsController = new PersonsController(personServiceMock.Object, contactInfoServiceMock.Object);

            var validPersonUpdateInput = new PersonUpdateInput { UUID = "1", FirstName = "John", LastName = "Doe", Company = "ABC Inc." };

            // Act
            var result = await personsController.Update(validPersonUpdateInput) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_WithValidPersonId_RedirectsToIndex()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var contactInfoServiceMock = new Mock<IContactInfoService>();

            var personsController = new PersonsController(personServiceMock.Object, contactInfoServiceMock.Object);

            var validPersonId = "1";

            // Act
            var result = await personsController.Delete(validPersonId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


    }
}
