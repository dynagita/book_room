using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Contract.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using Microsoft.AspNetCore.Authorization;

namespace BookRoom.WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    [Authorize]
    public class BookRoomController : Controller
    {
        private readonly IMediator _mediator;

        public BookRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for creating a new bookroom
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<BookRoomResponse>))]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Responsible for updating a bookroom
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<BookRoomResponse>))]
        public async Task<IActionResult> Update([FromQuery][Required]int id, CreateRoomRequest request)
        {
            request.Reference = id;
            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Responsible for canceling a bookroom
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 401, Type = typeof(CommonResponse<BookRoomResponse>))]
        [Route("cancel")]
        public async Task<IActionResult> Cancel([FromQuery][Required] int id)
        {
            var request = new CancelBookRoomRequest()
            {
                Reference = id
            };

            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
