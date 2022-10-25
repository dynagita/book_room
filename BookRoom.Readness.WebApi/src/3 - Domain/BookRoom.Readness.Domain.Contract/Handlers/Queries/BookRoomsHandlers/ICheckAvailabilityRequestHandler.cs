using BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Handlers.Queries.BookRoomsHandlers
{
    public interface ICheckAvailabilityRequestHandler : IRequestHandler<CheckAvailabilityRequest, CommonResponse<CheckAvailabilityResponse>>
    {
    }
}
