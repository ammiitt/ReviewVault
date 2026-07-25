using FluentValidation;
using ReviewVault.Application.DTOs.RequestDTOs;

namespace ReviewVault.Application.Validators
{
    public class CreatePostValidator : AbstractValidator<CreateRequestDTO>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters")
                .MaximumLength(200).WithMessage("Title can't exceed 200 characters");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required")
                .MinimumLength(50).WithMessage("Write at least 50 characters");

            RuleFor(x => x.Summary)
                .MaximumLength(500).WithMessage("Summary can't exceed 500 characters")
                .When(x => x.Summary != null);

            RuleFor(x => x.CoverImageUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid URL format")
                .When(x => !string.IsNullOrEmpty(x.CoverImageUrl));

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 (Terrible) and 5 (Masterpiece)");

            RuleFor(x => x.MediaTypeId)
                .GreaterThan(0).WithMessage("Select a media type");

            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("Select at least one category")
                .Must(ids => ids.All(id => id > 0)).WithMessage("Invalid category ID")
                .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage("Duplicate categories not allowed");
        }
    }
}
