using GymManagementSystem.Enums;
using GymManagementSystem.Models;

namespace GymManagementSystem.Responses
{
    public class SubscriptionResponse
    {
     
        public DateOnly JoinDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Plans Plans { get; set; }
        public Status Status { get; set; }
       

        public static SubscriptionResponse FromModel(Subscription subscription)
        {
            var response = new SubscriptionResponse
            {
               
                JoinDate = subscription.JoinDate,
                EndDate = subscription.EndDate,
                Plans = subscription.Plans,
                Status = subscription.Status
             
            };
            
            return response;
        }

        public static List<SubscriptionResponse> FromModels(List<Subscription> subscriptions)
        {
            return subscriptions.Select(subscription=>FromModel(subscription)).ToList();
        }
    }
}