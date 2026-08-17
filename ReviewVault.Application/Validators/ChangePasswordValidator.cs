using FluentValidation;
using ReviewVault.Application.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewVault.Application.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequestDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(6).WithMessage("Minimum 6 characters")
                .Matches("[A-Z]").WithMessage("Need an uppercase letter")
                .Matches("[0-9]").WithMessage("Need a number");
        }
    }
}
