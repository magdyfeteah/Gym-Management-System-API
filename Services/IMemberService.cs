using GymManagementSystem.Models;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using GymManagementSystem.Validation;

namespace GymManagementSystem.Services
{
    public interface IMemberService
    {
        Task<List<MemberResponse>> GetAllMembersAsync ();
        Task<MemberResponse>  GetMemberById(Guid memberId);
        Task <MemberResponse> CreateMemberAsync(CreateMemberRequest request  );
        Task UpdateMemberAsync( Guid memberId , UpdateMemberRequest request);
        Task DeleteMemberAsync (Guid memberId );
        Task CheckInAsync(Guid memberId);
    }
}