using GymManagementSystem.Enums;
using GymManagementSystem.Helper;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;
using GymManagementSystem.Validation;

namespace GymManagementSystem.Extensions
{
    public static class MemberExtensions
    {
        public static void UpdatedForm (this Member member,UpdateMemberRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.FullName))
            member.FullName = request.FullName;

            if (!string.IsNullOrWhiteSpace(request.Email))
                member.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Password))
                member.Password = request.Password; 

            if (request.Age.HasValue)
                member.Age = request.Age.Value;

            if (request.Weight.HasValue)
                member.Weight = request.Weight.Value;

            if (request.Height.HasValue)
                member.Height = request.Height.Value;

            if (request.IsPrivate.HasValue)
                member.IsPrivate = request.IsPrivate.Value;

            if (request.Subscription != null && member.Subscription != null){
                member.Subscription.Plans = request.Subscription.Plans?? member.Subscription.Plans;
                var joinDate = member.Subscription.JoinDate == default ? DateOnly.FromDateTime(DateTime.UtcNow) : member.Subscription.JoinDate;
                member.Subscription.EndDate = member.SubTimeHelper(joinDate);
                member.SubSessionsHelper();
                
                
            }
            
        }
    }
}