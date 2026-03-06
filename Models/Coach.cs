namespace GymManagementSystem.Models
{
    public class Coach : Person
    {
        
        public int ExperienceYears { get; set; }
        public List<Member>? Members { get; set; }
    }
}