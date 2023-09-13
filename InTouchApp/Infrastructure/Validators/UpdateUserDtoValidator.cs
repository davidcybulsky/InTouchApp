using FluentValidation;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;

namespace InTouchApi.Infrastructure.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.Role)
                .NotEmpty()
                .Must(value =>
                {
                    return (value == ROLES.ADMIN) || (value == ROLES.USER);
                })
                .WithMessage("Invalid role");
        }
    }
}
