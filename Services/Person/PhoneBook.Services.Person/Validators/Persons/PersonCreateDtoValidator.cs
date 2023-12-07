using FluentValidation;
using PhoneBook.Services.Person.Dtos.Persons;

namespace PhoneBook.Services.Person.Validators.Persons
{
    public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad Alanı boş olamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad Alanı boş olmaz");
        }
    }
}
