namespace GymManagementSystem.Models
{
    public class Member : Person
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int SessionsAvailable { get; set; }
        public bool IsPrivate { get; set; }
        public Guid? CoachId { get; set; }
        public Coach? Coach { get; set; }
        public Subscription Subscription { get; set; }



    }
}