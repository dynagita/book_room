using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Requests.Queries.RoomQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRoom.Readness.WebApi.Controllers
{
    /// <summary>
    /// Controller used to manage authentications endpoints
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for listing rooms
        /// </summary>
        /// <returns>Room's list</returns>
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<RoomResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<RoomResponse>))]
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new ListRoomsRequest());

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
