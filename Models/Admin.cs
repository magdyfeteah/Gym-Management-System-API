using GymManagementSystem.Enums;

namespace GymManagementSystem.Models
{
    public class Admin : Person
    {
        public Shifts Shifts { get; set; }
    }
}