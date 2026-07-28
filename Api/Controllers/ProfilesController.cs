using Application.DTOs.Profiles;
using Application.DTOs.Quizman;
using Application.Features.Profiles.DeleteQuizemanProfile;
using Application.Features.Profiles.GetAdminProfile;
using Application.Features.Profiles.GetMyProfile;
using Application.Features.Profiles.GetQuizmenList;
using Application.Features.Profiles.RestoreQuizemanProfile;
using Application.Features.Profiles.UpdateMyAdminProfile;
using Application.Features.Profiles.UpdateMyProfile;
using Application.Features.Profiles.UpdatePhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{profileId}/delete-quizman-profile")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DeleteQuizmanProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DeleteQuizmanProfileResponse> DeleteQuizmanProfile(Guid profileId)
        {
            return await _mediator.Send(new DeleteQuizmanProfileCommand { QuizmanId = profileId });
        }

        [HttpGet("{adminId}/admin-profile")]
        [Authorize]
        [ProducesResponseType(typeof(AdminProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AdminProfileResponse> GetAdminProfile(Guid adminId)
        {
            return await _mediator.Send(new GetAdminProfileQuery { AdminId = adminId });
        }

        [HttpGet("{quizmanId}/quizman-profile")]
        [Authorize]
        [ProducesResponseType(typeof(QuizmanProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<QuizmanProfileResponse> GetQuizmanProfile(Guid quizmanId)
        {
            return await _mediator.Send(new GetQuizmanProfileQuery { QuizmanId = quizmanId });
        }

        [HttpGet("all-quizmen-profiles")]
        [Authorize]
        [ProducesResponseType(typeof(QuizmenListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<QuizmenListResponse> GetQuizmenList([FromBody] GetQuizmenListQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut("{quizmanId}/restore")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(QuizmanProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<QuizmanProfileResponse> ResporeQuizmanProfile(Guid quizmanId)
        {
            return await _mediator.Send(new RestoreQuizmanProfileCommand { QuizmanId = quizmanId});
        }

        [HttpPut("update-my-admin-profile")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AdminProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<AdminProfileResponse> UpdateMyAdminProfile(
            [FromBody] UpdateMyAdminProfileCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("update-my-quizman-profile")]
        [Authorize(Roles = "Quizman")]
        [ProducesResponseType(typeof(QuizmanProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<QuizmanProfileResponse> UpdateMyQuizmanProfile(
            [FromBody] UpdateMyQuizmanProfileCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("update-photo")]
        [Authorize]
        [ProducesResponseType(typeof(UpdatePhotoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePhotoResponse> UpdatePhoto(
            [FromBody] UpdatePhotoCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
