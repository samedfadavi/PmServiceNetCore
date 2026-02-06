using FluentValidation;
using PmServiceNetCode.DTOs;

namespace PmServiceNetCode.Validation
{
    public class CreateFormDtoValidator : AbstractValidator<CreateFormDto>
    {
        public CreateFormDtoValidator()
        {
            RuleFor(x => x.Onvan)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }

}
