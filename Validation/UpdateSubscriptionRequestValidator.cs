using FluentValidation;
using GymManagementSystem.Requests;

namespace GymManagementSystem.Validation
{
    public class UpdateSubscriptionRequestValidator : AbstractValidator<UpdateSubscriptionRequest>
    {
        public UpdateSubscriptionRequestValidator()
        {
            RuleFor(s=>s.Plans)
            .IsInEnum().WithMessage("plans must be one of OneSession,OneMonth,ThreeMonth,SixMonth,OneYear.")
            .When(s=>s.Plans != null);

        }
    }
}