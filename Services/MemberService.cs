using System.Linq.Expressions;
using GymManagementSystem.Data;
using GymManagementSystem.Enums;
using GymManagementSystem.Extensions;
using GymManagementSystem.Helper;
using GymManagementSystem.Models;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using GymManagementSystem.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GymManagementSystem.Services
{
    public class MemberService(AppDbContext context) : IMemberService
    {
        private AppDbContext _context { get; } = context;
        public async Task<List<MemberResponse>> GetAllMembersAsync()
        {
            var members = await _context.Members.Include(m => m.Coach)
                .Include(m => m.Subscription)
                .ToListAsync();
            if (!members.Any())
            {
                throw new KeyNotFoundException("Member list is empty!");
            }
            return members.Select(m => MemberResponse.FromModel(m)).ToList();
        }

        public async Task<MemberResponse> GetMemberById(Guid memberId)
        {
            var member = await _context.Members.Include(m => m.Coach)
                .Include(m => m.Subscription)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member is null)
                throw new KeyNotFoundException("Member with this id not found!");


            return MemberResponse.FromModel(member);

        }
        public async Task<MemberResponse> CreateMemberAsync(CreateMemberRequest request)
        {
            Guid? coachId = null;
            if (request.IsPrivate)
            {
                var r = new Random();
                var coaches = await _context.Coaches.ToListAsync();
                var index = r.Next(coaches.Count());
                if (coaches.Any())
                {
                    coachId = coaches[index].Id;
                }
            }
            var member = new Member
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Phone = request.Phone,
                Age = request.Age,
                Gender = request.Gender,
                Weight = request.Weight,
                Height = request.Height,
                IsPrivate = request.IsPrivate,
                CoachId = coachId,


            };
            var joinDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var subscription = new Subscription
            {
                Plans = request.Subscription.Plans,
                JoinDate = joinDate
            };

            member.Subscription = subscription;

            subscription.EndDate = member.SubTimeHelper(joinDate);

            member.SubSessionsHelper();

            subscription.Status =
                subscription.EndDate > DateOnly.FromDateTime(DateTime.UtcNow)
                    ? Status.Active
                    : Status.Expired;

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return MemberResponse.FromModel(member);
        }
        public async Task UpdateMemberAsync(Guid memberId, UpdateMemberRequest request)
        {
            var member = await _context.Members.Include(m => m.Subscription)
                .FirstOrDefaultAsync(m => m.Id == memberId)
                ?? throw new KeyNotFoundException("Member with this id not found!");

            MemberExtensions.UpdatedForm(member, request);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteMemberAsync(Guid memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member with this id not found!");

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
        public async Task CheckInAsync(Guid memberId)
        {
            var member = await _context.Members
                .Include(m => m.Subscription)
                .Include(m => m.Coach)
                .FirstOrDefaultAsync(m => m.Id == memberId);

            if (member is null)
            {
                throw new KeyNotFoundException("Member not found!");
            }
            if (member.Subscription is null)
            {
                ;
                throw new InvalidOperationException("Subscription not found!");
            }
            if (member.SessionsAvailable <= 0)
            {
                throw new InvalidOperationException("No sessions available..");
            }
            if (DateOnly.FromDateTime(DateTime.UtcNow) > member.Subscription.EndDate)
            {
                member.Subscription.Status = Status.Expired;
                await _context.SaveChangesAsync();
                throw new InvalidOperationException("Subscription expired !!");
            }
            member.SessionsAvailable--;
            if (member.SessionsAvailable == 0)
                member.Subscription.Status = Status.Expired;

            await _context.SaveChangesAsync();
        }
    }
}