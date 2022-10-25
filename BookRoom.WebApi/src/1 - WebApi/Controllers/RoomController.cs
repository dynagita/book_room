using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using System.ComponentModel.DataAnnotations;

namespace BookRoom.WebApi.Controllers
{
    /// <summary>
    /// User controller, used to managed locked users endpoints
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for creating a new room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<UserResponse>))]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Responsible for editing a room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<UserResponse>))]
        public async Task<IActionResult> Update([FromQuery][Required] int id, UpdateRoomRequest request)
        {
            request.Reference = id;

            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Responsible for editing a room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<UserResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<UserResponse>))]
        public async Task<IActionResult> Delete([Required]int id)
        {
            var request = new DeleteRoomRequest()
            {
                Id = id
            };

            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
