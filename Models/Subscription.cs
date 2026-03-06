using GymManagementSystem.Enums;
using GymManagementSystem.Responses;

namespace GymManagementSystem.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Plans Plans { get; set; }
        public Status Status { get; set; }
        public Member Member { get; set; }
        public Guid MemberId { get; set; }

    }
}