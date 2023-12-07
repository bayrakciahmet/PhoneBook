using FluentValidation;
using MongoDB.Bson;
using PhoneBook.Services.Person.Dtos.Persons;

namespace PhoneBook.Services.Person.Validators.Persons
{
    public class PersonUpdateDtoValidator : AbstractValidator<PersonUpdateDto>
    {
        public PersonUpdateDtoValidator()
        {
            RuleFor(dto => dto.UUID)
                .NotEmpty().WithMessage("UUID boş olamaz.")
                .Must(BeValidHex).WithMessage("UUID geçerli bir 24 karakterli hex değeri olmalıdır.");

            RuleFor(dto => dto.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.");
        }
        private bool BeValidHex(string value)
        {
            if (value == null)
                return false;

            return ObjectId.TryParse(value, out _);
        }
    }
}
