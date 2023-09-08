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
                .MaximumLength(32);

            RuleFor(x => x.NewPassword)
                .Equal(x => x.ConfirmNewPassword)
                .WithMessage("Password and confirm password have to be the same");
        }
    }
}
