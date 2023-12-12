using FluentValidation;
using MongoDB.Bson;
using PhoneBook.Services.Person.Dtos.ContactInfos;

namespace PhoneBook.Services.Person.Validators.ContactInfos
{
    public class ContactInfoCreateDtoValidator : AbstractValidator<ContactInfoCreateDto>
    {
        public ContactInfoCreateDtoValidator()
        {
            RuleFor(dto => dto.PersonId)
                .NotEmpty().WithMessage("PersonId boş olamaz.")
                .Must(BeValidHex).WithMessage("PersonId geçerli bir 24 karakterli hex değeri olmalıdır.");

            RuleFor(dto => dto.InfoType)
                .NotEmpty().WithMessage("InfoType boş olamaz.")
                .Must(BeValidInfoType).WithMessage("Geçersiz InfoType değeri.");

            RuleFor(dto => dto.InfoContent)
                .NotEmpty().WithMessage("InfoContent boş olamaz.")
                .MaximumLength(100).WithMessage("InfoContent en fazla 100 karakter uzunluğunda olmalıdır."); ;
        }
        private bool BeValidHex(string value)
        {
            if (value == null)
                return false;
            return ObjectId.TryParse(value, out _);
        }
        private bool BeValidInfoType(string value)
        {
            string[] allowedTypes = { "Telefon", "E-mail", "Konum" };
            return !string.IsNullOrEmpty(value) && allowedTypes.Contains(value);
        }
    }
}
