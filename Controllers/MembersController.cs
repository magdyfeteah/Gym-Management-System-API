using System;
using System.Security.Claims;
using GymManagementSystem.Requests;
using GymManagementSystem.Responses;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GymManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]" )]
    public class MembersController(IMemberService memberService) : ControllerBase
    {
        private bool IsMemberNotAuthorized(Guid memberId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return User.IsInRole("Member") && userId != memberId;
        }
        [Authorize (Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<MemberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Get all members")]
        [EndpointDescription("Retrieves a list of all members. Only Admins can access this endpoint.")]
        public async Task<IActionResult> GetAllMembers()
        {
            var response  = await memberService.GetAllMembersAsync();
            return Ok(response);
        }

        [Authorize(Roles ="Member,Admin,Coach")]
        [HttpGet("{memberId:guid}" , Name = nameof(GetMemberById))]
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Get member by ID")]
        [EndpointDescription("Allows Admins and Coaches to view any member. Members can only view their own profile.")]
        public async Task<IActionResult> GetMemberById(Guid memberId)
        {
            if (IsMemberNotAuthorized(memberId))
            {
                return Forbid();
            }
            var response = await memberService.GetMemberById(memberId);
            
            return Ok(response);
        }
        [Authorize(Roles ="Member")]
        [HttpGet("me")]
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [EndpointSummary("Get my profile")]
        [EndpointDescription("Retrieves the profile of the currently logged-in member.")]

        public async Task<IActionResult> GetMyProfile()
        {
            var memberId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await memberService.GetMemberById(memberId);

            return Ok(response);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EndpointSummary("Create member")]
        [EndpointDescription("Creates a new member. Only Admins can perform this action.")]
        public async Task<IActionResult> Post([FromBody] CreateMemberRequest request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);
            var response = await memberService.CreateMemberAsync(request);

            return CreatedAtRoute(nameof(GetMemberById) ,new{memberId = response.Id} ,response);
        }
        [Authorize(Roles ="Admin,Member")]
        [HttpDelete("{memberId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [EndpointSummary("Delete member")]
        [EndpointDescription("Deletes a member. Admins can delete any member, Members can only delete themselves.")]
        public async Task<IActionResult> DeleteMember(Guid memberId)
        {
            if (IsMemberNotAuthorized(memberId))
            {
                return Forbid();
            }
            await memberService.DeleteMemberAsync(memberId);
        
            return NoContent();
        }
        [Authorize(Roles ="Admin,Member")]
        [HttpPatch("{memberId:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [EndpointSummary("Update member")]
        [EndpointDescription("Updates a member. Admins can update any member, Members can only update themselves.")]

        public async Task<IActionResult> UpdateMember(Guid memberId ,[FromBody] UpdateMemberRequest request)
        {
            if (IsMemberNotAuthorized(memberId))
            {
                return Forbid();
            }
            await memberService.UpdateMemberAsync(memberId, request);

            return Ok(new {Message = "Member is updated successfully."});
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("checkIn/{memberId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EndpointSummary("Check-in member")]
        [EndpointDescription("Marks a member as active and checked-in. Only Admins can perform this action.")]
        public async Task<IActionResult> CheckIn(Guid memberId)
        {
            await memberService.CheckInAsync(memberId);
            return Ok(new{message = "Member is active and has checked successfully."});
        }
    }
}