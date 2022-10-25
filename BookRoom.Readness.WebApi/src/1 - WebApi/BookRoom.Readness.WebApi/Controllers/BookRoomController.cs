using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRoom.Readness.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class BookRoomController : Controller
    {
        private readonly IMediator _mediator;

        public BookRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for listing book rooms
        /// </summary>
        /// <returns>Book Room's list</returns>
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<BookRoomResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<BookRoomResponse>))]
        public async Task<IActionResult> Index(string email)
        {
            var response = await _mediator.Send(new ListBooksByUserRequest()
            {
                Email = email
            });

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Responsible for checking availability
        /// </summary>
        /// <returns>Book Room's list</returns>
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<CheckAvailabilityResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<CheckAvailabilityResponse>))]
        [Route("/api/[controller]/availability")]
        public async Task<IActionResult> Index([FromQuery]CheckAvailabilityRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
