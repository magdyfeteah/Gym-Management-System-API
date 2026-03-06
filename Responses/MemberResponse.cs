using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;

namespace GymManagementSystem.Responses
{
    public class MemberResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int SessionsAvailable { get; set; }
        public bool IsPrivate { get; set; }
        public string? CoachName { get; set; }
        public SubscriptionResponse Subscription { get; set; }
    

    public static MemberResponse FromModel(Member member){

        var response = new MemberResponse{
                Id = member.Id,
                FullName = member.FullName,
                Age = member.Age,
                Phone = member.Phone,
                Gender = member.Gender,
                Email = member.Email,
                Weight = member.Weight,
                Height = member.Height,
                SessionsAvailable = member.SessionsAvailable,
                IsPrivate = member.IsPrivate,
                CoachName = member.IsPrivate && member.Coach != null ? member.Coach.FullName : null ,
                Subscription = member.Subscription != null ? SubscriptionResponse.FromModel(member.Subscription) : null!,
            };        
            return response;
            
    }

    public static List<MemberResponse> FromModels(List<Member> members)
        {
            return members.Select(member => FromModel(member)).ToList();
        }
    
}
}