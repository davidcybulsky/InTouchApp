﻿using FluentValidation;
using InTouchApi.Application.Models;
using InTouchApi.Infrastructure.Data;

namespace InTouchApi.Infrastructure.Validators
{
    public class SignUpDtoValidator : AbstractValidator<SignUpDto>
    {
        public SignUpDtoValidator(ApiContext apiContext)
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
                .MaximumLength(32);

            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Password and confirm password have to be the same");
        }
    }
}
