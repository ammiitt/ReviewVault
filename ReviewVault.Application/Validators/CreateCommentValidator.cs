using FluentValidation;
using ReviewVault.Application.DTOs.RequestDTOs;

namespace ReviewVault.Application.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentRequestDTO>
{
    public CreateCommentValidator()
        {
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Comment cannot be empty")
                .MinimumLength(2).WithMessage("Comment is too short")
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters");

            RuleFor(x => x.PostId)
                .GreaterThan(0).WithMessage("Invalid post");
        }
    }
}
