using FluentValidation;
using InTouchApi.Application.Models;
using InTouchApi.Infrastructure.Data;

namespace InTouchApi.Infrastructure.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(ApiContext apiContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(email =>
                {
                    var result = apiContext.Users.Any(x => x.Email == email);
                    return !result;
                });

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(32)
                .Equal(x => x.ConfirmPassword);
        }
    }
}
