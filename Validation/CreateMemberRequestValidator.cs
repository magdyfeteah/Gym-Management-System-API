using System.Data;
using FluentValidation;
using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;

namespace GymManagementSystem.Validation
{
    public class CreateMemberRequestValidator : AbstractValidator<CreateMemberRequest>
    {
        public CreateMemberRequestValidator()
        {
            RuleFor(m=>m.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Member full name is required.")
            .Length(3,255).WithMessage("Full name must be between 3 and 255 characters long.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Full name can only contain letters and spaces.")
            .Must(name => name== name.Trim()).WithMessage("Full name must not start or end with spaces.");

            RuleFor(m=>m.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Member email is required.")
            .Length(3,100).WithMessage("Email must be between 3 and 100 characters long.")
            .EmailAddress().WithMessage("Please enter a valid email address (e.g., user@example.com).");

            RuleFor(m=>m.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Member password is required.")
            .Length(8,18).WithMessage("Password must be between 8 and 18 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase character.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase character.")
            .Matches(@"[\d]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character (e.g., !, @, #).");
            
            RuleFor(m=>m.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Member phone is required.")
            .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits and contain only numbers.");
            
            RuleFor(m=>m.Age)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Member age is required.")
            .GreaterThanOrEqualTo(14).WithMessage("Age must be greater than or equal to 14.")
            .LessThanOrEqualTo(80).WithMessage("Age must not exceed 80 years.");
            
            RuleFor(m=>m.Gender)
            .Cascade(CascadeMode.Stop)
            .IsInEnum().WithMessage("Gender must be either Male or Female.");
            
            RuleFor(m => m.Weight)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Weight is required.")
            .GreaterThanOrEqualTo(30).WithMessage("Weight must be at least 30 kg.")
            .LessThanOrEqualTo(200).WithMessage("Weight must not exceed 200 kg.");

            RuleFor(m => m.Height)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Height is required.")
            .GreaterThanOrEqualTo(100).WithMessage("Height must be at least 100 cm.")
            .LessThanOrEqualTo(250).WithMessage("Height must not exceed 250 cm.");

            RuleFor(m=>m.IsPrivate)
            .NotNull();

            RuleFor(m=>m.Subscription)
            .NotNull().WithMessage("Subscription is required.")
            .SetValidator(new CreateSubscriptionRequestValidator());
        }
    }
}