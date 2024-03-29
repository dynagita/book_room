﻿using BookRoom.Readness.Domain.Contract.Requests.Queries.AuthQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.AuthResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRoom.Readness.WebApi.Controllers
{
    /// <summary>
    /// Controller used to manage authentications endpoints
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Responsible for creating a token for user access locked endpoints
        /// </summary>
        /// <param name="request">User informations, e-mail and password</param>
        /// <returns>Token and user informations</returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(CommonResponse<AuthResponse>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(CommonResponse<AuthResponse>))]
        public async Task<IActionResult> Index(AuthRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Status == 200)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
