using FluentValidation;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Validators
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(32)
                .Equal(x => x.ConfirmNewPassword);
        }
    }
}
