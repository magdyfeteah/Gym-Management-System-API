using GymManagementSystem.Enums;
using GymManagementSystem.Models;

namespace GymManagementSystem.Helper
{
    public static class MemberHelper
    {
        public static DateOnly SubTimeHelper(this Member member, DateOnly joinDate )
        {
            var endDate = member.Subscription.Plans switch
            {
                Plans.OneSession => joinDate.AddDays(1),
                Plans.OneMonth => joinDate.AddMonths(1),
                Plans.ThreeMonth => joinDate.AddMonths(3),
                Plans.SixMonth => joinDate.AddMonths(6),
                Plans.OneYear => joinDate.AddYears(1),
                _ => joinDate
            };
            member.Subscription.Status = Status.Active;

            return  endDate;
        }
        public static void SubSessionsHelper(this Member member)
        {
            member.SessionsAvailable = member.Subscription.Plans switch
            {
                Plans.OneSession => 1,
                Plans.OneMonth => 12 ,
                Plans.ThreeMonth => 36 ,
                Plans.SixMonth => 72,
                Plans.OneYear => 144,
                _ => 0
            };
            member.Subscription.Status = Status.Active;
        }
    }

}