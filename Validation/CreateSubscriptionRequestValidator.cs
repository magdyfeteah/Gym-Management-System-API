using FluentValidation;
using GymManagementSystem.Requests;

namespace GymManagementSystem.Validation
{
    public class CreateSubscriptionRequestValidator : AbstractValidator<CreateSubscriptionRequest>
    {
        public CreateSubscriptionRequestValidator()
        {
            RuleFor(s=>s.Plans)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Member plans is required.")
            .IsInEnum().WithMessage("plans must be one of OneSession,OneMonth,ThreeMonth,SixMonth,OneYear.");
        }
    }
}