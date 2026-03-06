using GymManagementSystem.Enums;
using GymManagementSystem.Models;

namespace GymManagementSystem.Requests
{
    public class CreateMemberRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool IsPrivate { get; set; }
        public CreateSubscriptionRequest Subscription { get; set; }

    }
}
