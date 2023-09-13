using FluentValidation;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;

namespace InTouchApi.Infrastructure.Validators
{
    public class UpdateReactionDtoValidator : AbstractValidator<UpdateReactionDto>
    {
        public UpdateReactionDtoValidator()
        {
            RuleFor(c => c.ReactionType)
                .NotEmpty()
                .Must(value =>
                {
                    return (value == REACTIONS.LIKE) || (value == REACTIONS.DISLIKE);
                })
                .WithMessage("Invalid value");
        }
    }
}
