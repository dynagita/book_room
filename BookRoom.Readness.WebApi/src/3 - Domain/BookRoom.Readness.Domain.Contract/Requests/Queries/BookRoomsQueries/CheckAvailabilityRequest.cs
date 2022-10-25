using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using MediatR;

namespace BookRoom.Readness.Domain.Contract.Requests.Queries.BookRoomsQueries
{
    public class CheckAvailabilityRequest : IRequest<CommonResponse<CheckAvailabilityResponse>>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RoomNumber { get; set; }
    }
}
