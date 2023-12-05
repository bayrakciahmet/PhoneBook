using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Services.Person.Controllers;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Services.Interfaces;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Test.Controllers
{
    public class PersonsControllerTest
    {
        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResultWithCreatedPerson()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var controller = new PersonsController(personServiceMock.Object);

            var personCreateDto = new PersonCreateDto { FirstName = "John", LastName = "Doe" };
            var newPersonDto = new PersonDto { UUID = "1", FirstName = "John", LastName = "Doe", CreatedTime = DateTime.Now };

            var response = Response<PersonDto>.Success(newPersonDto, 201);
            personServiceMock.Setup(m => m.CreateAsync(personCreateDto)).ReturnsAsync(response);

            // Act
            var result = await controller.Create(personCreateDto);

            // Assert
            var createdAtActionResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<PersonDto>>(createdAtActionResult.Value);

            Assert.Equal(response.StatusCode, createdAtActionResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // GetById
        [Fact]
        public async Task GetById_ShouldReturnOkResultWithPerson()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var controller = new PersonsController(personServiceMock.Object);

            var personId = "1";
            var personDto = new PersonDto { UUID = "1", FirstName = "John", LastName = "Doe", CreatedTime = DateTime.Now };

            var response = Response<PersonDto>.Success(personDto, 200);
            personServiceMock.Setup(m => m.GetByIdAsync(personId)).ReturnsAsync(response);

            // Act
            var result = await controller.GetById(personId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<PersonDto>>(objectResult.Value);

            Assert.True(objectResult.StatusCode.HasValue);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // GetAll
        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithPersonList()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var controller = new PersonsController(personServiceMock.Object);

            var persons = new List<PersonDto>
    {
        new PersonDto { UUID = "1", FirstName = "John", LastName = "Doe", CreatedTime = DateTime.Now },
        new PersonDto { UUID = "2", FirstName = "Jane", LastName = "Doe", CreatedTime = DateTime.Now }
    };

            var response = Response<List<PersonDto>>.Success(persons, 200);
            personServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(response);

            // Act
            var result = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<List<PersonDto>>>(objectResult.Value);

            Assert.True(objectResult.StatusCode.HasValue);
            Assert.Equal(response.StatusCode, objectResult.StatusCode);
            Assert.Equal(response.Data, model.Data);
            Assert.True(model.IsSuccessful);
        }

        // Update
        [Fact]
        public async Task Update_ShouldReturnNoContentResult()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var controller = new PersonsController(personServiceMock.Object);

            var personUpdateDto = new PersonUpdateDto { UUID = "1", FirstName = "Updated John", LastName = "Updated Doe" };

            var response = Response<NoContent>.Success(204);
            personServiceMock.Setup(m => m.UpdateAsync(personUpdateDto)).ReturnsAsync(response);

            // Act
            var result = await controller.Update(personUpdateDto);

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(response.StatusCode, noContentResult.StatusCode);
        }

        // Delete
        [Fact]
        public async Task Delete_ShouldReturnNoContentResult()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var controller = new PersonsController(personServiceMock.Object);

            var personId = "1";

            var response = Response<NoContent>.Success(204);
            personServiceMock.Setup(m => m.DeleteAsync(personId)).ReturnsAsync(response);

            // Act
            var result = await controller.Delete(personId);

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(response.StatusCode, noContentResult.StatusCode);
        }

    }
}
