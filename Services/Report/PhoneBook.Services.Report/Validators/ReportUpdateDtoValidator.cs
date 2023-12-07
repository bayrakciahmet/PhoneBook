using FluentValidation;
using PhoneBook.Services.Report.Dtos;

namespace PhoneBook.Services.Report.Validators
{
    public class ReportUpdateDtoValidator : AbstractValidator<ReportUpdateDto>
    {
        public ReportUpdateDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır.");

            RuleFor(dto => dto.ReportName)
                .NotEmpty().WithMessage("ReportName boş olamaz.")
                .MaximumLength(200).WithMessage("ReportName en fazla 200 karakter uzunluğunda olmalıdır.");
        }
    }
}
