using System.Data;
using FluentValidation;
using GymManagementSystem.Requests;

namespace GymManagementSystem.Validation
{
    public class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberRequest>
    {
        public UpdateMemberRequestValidator()
        {
            RuleFor(m=>m.FullName)
            .Cascade(CascadeMode.Stop)
            .Length(3,255).WithMessage("Full name must be between 3 and 255 characters long.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Full name can only contain letters and spaces.")
            .Must(name => name== name.Trim()).WithMessage("Full name must not start or end with spaces.")
            .When(m=>m.FullName != null);


            RuleFor(m=>m.Email)
            .Cascade(CascadeMode.Stop)
            .Length(3,100).WithMessage("Email must be between 3 and 100 characters long.")
            .EmailAddress().WithMessage("Please enter a valid email address (e.g., user@example.com).")
             .When(m=>m.Email != null);

            RuleFor(m=>m.Password)
            .Cascade(CascadeMode.Stop)
            .Length(8,18).WithMessage("Password must be between 8 and 18 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase character.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase character.")
            .Matches(@"[\d]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character (e.g., !, @, #).")
            .When(m=>m.Password != null);

            RuleFor(m=>m.Phone)
            .Cascade(CascadeMode.Stop)
            .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits and contain only numbers.")
            .When(m=>m.Phone != null);

            RuleFor(m=>m.Age)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(14).WithMessage("Age must be greater than or equal to 14.")
            .LessThanOrEqualTo(80).WithMessage("Age must not exceed 80 years.")
            .When(m=>m.Age != null);
            
            RuleFor(m => m.Weight)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(30).WithMessage("Weight must be at least 30 kg.")
            .LessThanOrEqualTo(200).WithMessage("Weight must not exceed 200 kg.")
            .When(m=>m.Weight != null);

            RuleFor(m => m.Height)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(100).WithMessage("Height must be at least 100 cm.")
            .LessThanOrEqualTo(250).WithMessage("Height must not exceed 250 cm.")
            .When(m=>m.Height != null);

            RuleFor(m=>m.IsPrivate)
            .NotNull()
            .When(m=>m.IsPrivate != null);

            RuleFor(m=>m.Subscription)
            .SetValidator(new UpdateSubscriptionRequestValidator()!)
            .When(m=> m.Subscription != null);
            
        }
    }
}