using FluentValidation;
using PhoneBook.Services.Report.Dtos;

namespace PhoneBook.Services.Report.Validators
{
    public class ReportCreateDtoValidator : AbstractValidator<ReportCreateDto>
    {
        public ReportCreateDtoValidator()
        {
            RuleFor(dto => dto.ReportName)
                .NotEmpty().WithMessage("ReportName boş olamaz.")
                .MaximumLength(200).WithMessage("ReportName en fazla 200 karakter uzunluğunda olmalıdır.");
        }
    }
}
